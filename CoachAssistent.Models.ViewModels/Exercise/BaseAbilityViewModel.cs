using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Exercise
{
    public class BaseAbilityViewModel
    {
        readonly string _modelName;
        public BaseAbilityViewModel(string modelName)
        {
            _modelName = modelName;
        }

        public Constructor Constructor { get { return new Constructor(_modelName); } }
    }

    public class Constructor
    {
        readonly string _modelName;
        public Constructor(string modelName)
        {
            _modelName = modelName;
        }
        public string ModelName { get { return _modelName; } }
    }
}
