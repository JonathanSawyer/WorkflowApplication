﻿using BL;
using BL.Workflow;
using FluentNHibernate.Mapping;

namespace RateIT.Example.DalMappings
{
    public class UserWorkflowUpdateMap : SubclassMap<UserWorkflowUpdate>
    {
        public UserWorkflowUpdateMap()
        {
            DiscriminatorValue(WorkflowType.Update);
            Component(x => x.UserData, m =>
            {
                UserBaseClassMap.InitializeUserData(m);
            });
        }
    }
}