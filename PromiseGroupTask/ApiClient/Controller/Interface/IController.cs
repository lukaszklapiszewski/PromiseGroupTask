using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Controller.Interface
{
    internal interface IController
    {
        int CurrentPage {  get; set; }
    }
}
