﻿using System.Collections.Generic;

namespace Microsoft.FeatureManagement
{
    class AssignerSettings : IAssignerSettings
    {
        public string Name { get; set; }

        public IEnumerable<AssignmentChoice> Assignments { get; set; }
    }
}
