﻿using CantinaApp.Core.DomainServices;
using CantinaApp.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CantinaApp.InfaStructure.Data.SQLRepositories
{
    public class SQLSpecialOffersRepositories : ISpecialOffersRepositories
    {
        readonly CantinaAppContext _ctx;

        public SQLSpecialOffersRepositories(CantinaAppContext ctx)
        {
            _ctx = ctx;
        }

        public SpecialOffers CreateSpecialOffers(SpecialOffers specialOffers)
        {
            _ctx.Attach(specialOffers).State = EntityState.Added;
            _ctx.SaveChanges();
            return specialOffers;
        }

        public SpecialOffers ReadByIdIncludeIngr(int id)
        {
            return _ctx.SpecialOffers
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<SpecialOffers> ReadSpecialOffers()
        {
            return _ctx.SpecialOffers;
        }

        public SpecialOffers UpdateSpecialOffers(SpecialOffers specialOffers)
        {
            _ctx.Attach(specialOffers).State = EntityState.Modified;
            _ctx.SaveChanges();
            return specialOffers;
        }

        public SpecialOffers DeleteSpecialOffers(int id)
        {
            var specialDelete = _ctx.SpecialOffers.ToList().FirstOrDefault(b => b.Id == id);
            _ctx.SpecialOffers.Remove(specialDelete);
            _ctx.SaveChanges();
            return specialDelete;
        }

        public IEnumerable<SpecialOffers> ReadTodaySpecielOffers(DateTime date)
        {
                return _ctx.SpecialOffers.Where(c => c.OffersDate.Date == date.Date);
            
        }
    }
}
