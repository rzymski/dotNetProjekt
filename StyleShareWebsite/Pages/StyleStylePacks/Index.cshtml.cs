using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.StyleStylePacks.StyleStylePacks
{
    public class IndexModel : PageModel
    {

        public readonly IPostableService _postableService;
        public readonly IStyleService _styleService;
        public readonly IStyleStylePackService _styleStylePackService;
        public readonly IStylePackService _stylePackService;
        public readonly ILikeService _likeService;
        private readonly IUserService _userService;

        public IndexModel(IStyleStylePackService styleStylePackService, IPostableService postableService, IStyleService styleService, IStylePackService stylePackService ,ILikeService likeService, IUserService userService)
        {
            _styleStylePackService = styleStylePackService;
            _postableService = postableService;
            _styleService = styleService;
            _likeService = likeService;
            _userService = userService;
            _stylePackService = stylePackService;
        }

        public class StylePackWithStylesClass
        {
            public List<Style> Styles { get; set; }
            public StylePack StylePack { get; set; }

            //public StylePackWithStylesClass(StylePack stylePack, List<Style> styles)
            //{
            //    Styles = styles;
            //    StylePacks = stylePack;
            //}
            public StylePackWithStylesClass(List<StyleStylePack> styleStylePacks)
            {
                //Styles = styleStylePack.
                StylePack = styleStylePacks[0].StylePack;
                foreach(var s in styleStylePacks)
                {
                    Styles.Add(s.Style);
                }
            }

        }

        public List<StyleStylePack> StyleStylePacks { get;set; }
        public List<Style> Styles { get;set; }
        public List<StylePack> StylePacks { get;set; }
        public List<StylePackWithStylesClass> StylePackWithStyles { get;set; } = new List<StylePackWithStylesClass>();

        public async Task OnGetAsync()
        {
            //StylePacks = _stylePackService.GetAllAsync().Result.ToList();
            //foreach(var sp in StylePacks)
            //{
            //    StyleStylePacks = _styleStylePackService.GetAllStylesByStylePackId(sp.Id).Result.ToList();
            //}

            //foreach(var ssp in StyleStylePacks)
            //{
            //    StylePacks.Add(_stylePackService.GetByIdAsync(ssp.Id).Result);
            //}

            StylePacks = _stylePackService.GetAllAsync().Result.ToList();
            foreach(StylePack stylePack in StylePacks) 
            {
                StyleStylePacks = _styleStylePackService.GetAllStylesByStylePackId(stylePack.Id).Result.ToList();
                StylePackWithStyles.Add(new StylePackWithStylesClass(StyleStylePacks));
            }
        }
    }
}
