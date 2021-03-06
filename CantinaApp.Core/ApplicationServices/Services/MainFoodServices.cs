﻿using System;
using System.Collections.Generic;
using CantinaApp.Core.DomainServices;
using CantinaApp.Core.Entity.Entities;

namespace CantinaApp.Core.ApplicationServices.Services
{
    public class MainFoodServices : IMainFoodServices
    {
        readonly IMainFoodRepositories _mainFoodRepo;
        readonly IIngredientsRepositories _ingredientsRepo;
        private readonly IAllergensRepositories _allergensRepo;

        public MainFoodServices(IMainFoodRepositories mainFoodRepo,
            IIngredientsRepositories ingredientsRepo,
            IAllergensRepositories allergensRepo)
        {
            _mainFoodRepo = mainFoodRepo;
            _ingredientsRepo = ingredientsRepo;
            _allergensRepo = allergensRepo;
        }

        public MainFood AddMainFood(MainFood mainFood)
        {
            if (string.IsNullOrEmpty(mainFood.MainFoodName))
            {
                throw new ArgumentException("Main Food needs a name.");
            }
            return _mainFoodRepo.CreateMainFood(mainFood);
        }

        public MainFood DeleteMainFood(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID requires to be greater than 0.");
            }
            return _mainFoodRepo.DeleteMainFood(id);
        }

        public MainFood FindMainFoodIdIncludeRecipAlrg(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID requires to be greater than 0.");
            }

            return _mainFoodRepo.ReadByIdIncludeRecipAlrg(id);
        }

        public IEnumerable<MainFood> GetMainFood()
        {
            return _mainFoodRepo.ReadMainFood();
        }

        public IEnumerable<MainFood> GetTodayFood(DateTime date)
        {
            return _mainFoodRepo.ReadTodayMenues(date);
        }

        public MainFood UpdateMainFood(MainFood mainFoodUpdate)
        {
            if (string.IsNullOrEmpty(mainFoodUpdate.MainFoodName))
            {
                throw new ArgumentException("Main Food needs a name.");
            }
            else if (mainFoodUpdate.Id < 1)
            {
                throw new ArgumentException("You need to have an higher id than 0");
            }
            return _mainFoodRepo.UpdateMainFood(mainFoodUpdate);
        }
    }
}
