using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHelpers
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}
