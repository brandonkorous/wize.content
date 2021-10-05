using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wize.content.data.v1.Models;

namespace wize.content.odata.ModelConfigurations
{
    public class LinkGroupModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion version, string routePrefix)
        {
            switch(version.MajorVersion)
            {
                case 1:
                    BuildV1(builder);
                    break;
                default:
                    BuildDefault(builder);
                    break;
            }
        }

        private EntityTypeConfiguration<LinkGroup> BuildDefault(ODataModelBuilder builder)
        {
            var model = builder.EntitySet<LinkGroup>("LinkGroups").EntityType;
            model.HasKey(m => m.LinkGroupId);
            return model;
        }

        private void BuildV1(ODataModelBuilder builder)
        {
            BuildDefault(builder);//.Ignore(something);
        }
    }
}
