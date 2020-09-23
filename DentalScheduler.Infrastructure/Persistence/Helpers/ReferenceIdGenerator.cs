using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DentalScheduler.Infrastructure.Persistence.Helpers
{
    public class ReferenceIdGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

        protected override object NextValue([NotNullAttribute] EntityEntry entry)
        {
            return Guid.NewGuid();
        }
    }
}