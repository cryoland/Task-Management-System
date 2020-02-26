using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TMS.Application.Common.Models;

namespace TMS.WebUI.Common
{
    public static class SelectListFromList
    {
        public static SelectList ToSelectList<T>(this IList<T> list)
        {
            return new SelectList(list, nameof(FrameDto.Value), nameof(FrameDto.Name), list.FirstOrDefault());
        }
        public static SelectList ToSelectList<T>(this IList<T> list, object selectedValue)
        {
            return new SelectList(list, nameof(FrameDto.Value), nameof(FrameDto.Name), selectedValue ?? list.FirstOrDefault());
        }
    }
}