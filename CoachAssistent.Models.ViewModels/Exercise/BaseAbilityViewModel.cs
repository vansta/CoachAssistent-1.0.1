using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Exercise
{
    public class BaseAbilityViewModel(string modelName)
    {
        readonly string _modelName = modelName;

        public Constructor Constructor { get { return new Constructor(_modelName); } }
    }

    public class Constructor(string modelName)
    {
        readonly string _modelName = modelName;

        public string ModelName { get { return _modelName; } }
    }
}
