using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wize.content.data.v1.Models;

namespace wize.content.odata.ModelConfigurations
{
    public class ContentArchiveModelConfiguration : IModelConfiguration
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

        private EntityTypeConfiguration<ContentArchive> BuildDefault(ODataModelBuilder builder)
        {
            var model = builder.EntitySet<ContentArchive>("ContentArchives").EntityType;
            model.HasKey(m => m.ContentArchiveId);
            return model;
        }

        private void BuildV1(ODataModelBuilder builder)
        {
            BuildDefault(builder);//.Ignore(something);
        }
    }
}
