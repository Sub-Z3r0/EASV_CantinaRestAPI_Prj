﻿using CantinaApp.Core.DomainServices;
using CantinaApp.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CantinaApp.InfaStructure.Data.SQLRepositories
{
    public class SQLIngredientsRepositories : IIngredientsRepositories
    {
        readonly CantinaAppContext _ctx;

        public SQLIngredientsRepositories(CantinaAppContext ctx)
        {
            _ctx = ctx;
        }

        public Ingredients CreateIngredient(Ingredients ingredient)
        {
            _ctx.Attach(ingredient).State = EntityState.Added;
            _ctx.SaveChanges();
            return ingredient;
        }

        public Ingredients ReadByIdIncludeAllergens(int id)
        {
            return _ctx.Ingredients
                    .Include(c => c.RecipeLines)
                    .ThenInclude(c => c.MainFoodType)
                    .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Ingredients> ReadIngredients()
        {
            return _ctx.Ingredients;
        }

        public Ingredients UpdateIngredient(Ingredients ingredientUpdate)
        {
            foreach (var item in _ctx.RecipeLine.ToList().Where(c=>c.IngredientsId==ingredientUpdate.Id))
            {
                    _ctx.RecipeLine.Remove(item);
            }
            
            //Clone orderlines to new location in memory, so they are not overridden on Attach
            var newRecipeLines = new List<RecipeLine>(ingredientUpdate.RecipeLines);
            //Attach order so basic properties are updated
            _ctx.Attach(ingredientUpdate).State = EntityState.Modified;
            //Add all orderlines with updated order information
            
            foreach (var ol in newRecipeLines)
            {
                
                    _ctx.Entry(ol).State = EntityState.Added;
            }
            // Save it
            _ctx.SaveChanges();
            //Return it
            return ingredientUpdate;
        }

        public Ingredients DeleteIngredient(int id)
        {
            var ingrDelete = _ctx.Ingredients.ToList().FirstOrDefault(b => b.Id == id);
            _ctx.Ingredients.Remove(ingrDelete);
            _ctx.SaveChanges();
            return ingrDelete;
        }
    }
}
