using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.ViewModels
{
    public class IndexProductView
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public IEnumerable<CardsViewModel> Cards { get; set; }
    }

    public class CardsViewModel
    {   
        public string ImageSrcMain { get; set; }
        public string ImageSrcSecond { get; set; }
        public string CategoryName { get; set; }
        public string CategoryID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }
        public int StarsForLike { get; set; }  

        //偉軒用
        public double CountOfRentedDays { get; set; }
        //public bool IsExisted { get; set; }

    }

    public class FilterSearchViewModel:CardsViewModel
    {
        public List<CardsViewModel> SelectedProductList { get; set; }
        [Display(Name = "關鍵字搜尋")]
        public string Keyword { get; set; }

        [Display(Name = "種類篩選")]
        public string Category { get; set; }
        public string SubCategory { get; set; }

        [Display(Name = "排序方式")]
        public string OrderBy{ get; set; }

        [Display(Name = "錢錢決定一切")]
        public string RateBudget { get; set; }

    }

    public enum Pages
    {
        CategoriesCardsPage, ProductCardsPage
    }
    public enum Container
    {
        CategoriesCardsContainer, ProductCardsContainer
    }
    public enum ContainerTitle
    {
        種類列表, 所有商品, 您要的商品, 很抱歉找不到您要的商品
    }

    public class Rootobject
    {
        public string type { get; set; }
        public string format { get; set; }
        public string version { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Champion Aatrox { get; set; }
        public Champion Ahri { get; set; }
        public Champion Akali { get; set; }
        public Champion Alistar { get; set; }
        public Champion Amumu { get; set; }
        public Champion Anivia { get; set; }
        public Champion Annie { get; set; }
        public Champion Aphelios { get; set; }
        public Champion Ashe { get; set; }
        public Champion AurelionSol { get; set; }
        public Champion Azir { get; set; }
        public Champion Bard { get; set; }
        public Champion Blitzcrank { get; set; }
        public Champion Brand { get; set; }
        public Champion Braum { get; set; }
        public Champion Caitlyn { get; set; }
        public Champion Camille { get; set; }
        public Champion Cassiopeia { get; set; }
        public Champion Chogath { get; set; }
        public Champion Corki { get; set; }
        public Champion Darius { get; set; }
        public Champion Diana { get; set; }
        public Champion Draven { get; set; }
        public Champion DrMundo { get; set; }
        public Champion Ekko { get; set; }
        public Champion Elise { get; set; }
        public Champion Evelynn { get; set; }
        public Champion Ezreal { get; set; }
        public Champion Fiddlesticks { get; set; }
        public Champion Fiora { get; set; }
        public Champion Fizz { get; set; }
        public Champion Galio { get; set; }
        public Champion Gangplank { get; set; }
        public Champion Garen { get; set; }
        public Champion Gnar { get; set; }
        public Champion Gragas { get; set; }
        public Champion Graves { get; set; }
        public Champion Hecarim { get; set; }
        public Champion Heimerdinger { get; set; }
        public Champion Illaoi { get; set; }
        //public Irelia Irelia { get; set; }
        //public Ivern Ivern { get; set; }
        //public Janna Janna { get; set; }
        //public Jarvaniv JarvanIV { get; set; }
        //public Jax Jax { get; set; }
        //public Jayce Jayce { get; set; }
        //public Jhin Jhin { get; set; }
        //public Jinx Jinx { get; set; }
        //public Kaisa Kaisa { get; set; }
        //public Kalista Kalista { get; set; }
        //public Karma Karma { get; set; }
        //public Karthus Karthus { get; set; }
        //public Kassadin Kassadin { get; set; }
        //public Katarina Katarina { get; set; }
        //public Kayle Kayle { get; set; }
        //public Kayn Kayn { get; set; }
        //public Kennen Kennen { get; set; }
        //public Khazix Khazix { get; set; }
        //public Kindred Kindred { get; set; }
        //public Kled Kled { get; set; }
        //public Kogmaw KogMaw { get; set; }
        //public Leblanc Leblanc { get; set; }
        //public Leesin LeeSin { get; set; }
        //public Leona Leona { get; set; }
        //public Lillia Lillia { get; set; }
        //public Lissandra Lissandra { get; set; }
        //public Lucian Lucian { get; set; }
        //public Lulu Lulu { get; set; }
        //public Lux Lux { get; set; }
        //public Malphite Malphite { get; set; }
        //public Malzahar Malzahar { get; set; }
        //public Maokai Maokai { get; set; }
        //public Masteryi MasterYi { get; set; }
        //public Missfortune MissFortune { get; set; }
        //public Monkeyking MonkeyKing { get; set; }
        //public Mordekaiser Mordekaiser { get; set; }
        //public Morgana Morgana { get; set; }
        //public Nami Nami { get; set; }
        //public Nasus Nasus { get; set; }
        //public Nautilus Nautilus { get; set; }
        //public Neeko Neeko { get; set; }
        //public Nidalee Nidalee { get; set; }
        //public Nocturne Nocturne { get; set; }
        //public Nunu Nunu { get; set; }
        //public Olaf Olaf { get; set; }
        //public Orianna Orianna { get; set; }
        //public Ornn Ornn { get; set; }
        //public Pantheon Pantheon { get; set; }
        //public Poppy Poppy { get; set; }
        //public Pyke Pyke { get; set; }
        //public Qiyana Qiyana { get; set; }
        //public Quinn Quinn { get; set; }
        //public Rakan Rakan { get; set; }
        //public Rammus Rammus { get; set; }
        //public Reksai RekSai { get; set; }
        //public Renekton Renekton { get; set; }
        //public Rengar Rengar { get; set; }
        //public Riven Riven { get; set; }
        //public Rumble Rumble { get; set; }
        //public Ryze Ryze { get; set; }
        //public Samira Samira { get; set; }
        //public Sejuani Sejuani { get; set; }
        //public Senna Senna { get; set; }
        //public Seraphine Seraphine { get; set; }
        //public Sett Sett { get; set; }
        //public Shaco Shaco { get; set; }
        //public Shen Shen { get; set; }
        //public Shyvana Shyvana { get; set; }
        //public Singed Singed { get; set; }
        //public Sion Sion { get; set; }
        //public Sivir Sivir { get; set; }
        //public Skarner Skarner { get; set; }
        //public Sona Sona { get; set; }
        //public Soraka Soraka { get; set; }
        //public Swain Swain { get; set; }
        //public Sylas Sylas { get; set; }
        //public Syndra Syndra { get; set; }
        //public Tahmkench TahmKench { get; set; }
        //public Taliyah Taliyah { get; set; }
        //public Talon Talon { get; set; }
        //public Taric Taric { get; set; }
        //public Teemo Teemo { get; set; }
        //public Thresh Thresh { get; set; }
        //public Tristana Tristana { get; set; }
        //public Trundle Trundle { get; set; }
        //public Tryndamere Tryndamere { get; set; }
        //public Twistedfate TwistedFate { get; set; }
        //public Twitch Twitch { get; set; }
        //public Udyr Udyr { get; set; }
        //public Urgot Urgot { get; set; }
        //public Varus Varus { get; set; }
        //public Vayne Vayne { get; set; }
        //public Veigar Veigar { get; set; }
        //public Velkoz Velkoz { get; set; }
        //public Vi Vi { get; set; }
        //public Viktor Viktor { get; set; }
        //public Vladimir Vladimir { get; set; }
        //public Volibear Volibear { get; set; }
        //public Warwick Warwick { get; set; }
        //public Xayah Xayah { get; set; }
        //public Xerath Xerath { get; set; }
        //public Xinzhao XinZhao { get; set; }
        //public Yasuo Yasuo { get; set; }
        //public Yone Yone { get; set; }
        //public Yorick Yorick { get; set; }
        //public Yuumi Yuumi { get; set; }
        //public Zac Zac { get; set; }
        //public Zed Zed { get; set; }
        //public Ziggs Ziggs { get; set; }
        //public Zilean Zilean { get; set; }
        //public Zoe Zoe { get; set; }
        //public Zyra Zyra { get; set; }
    }

    public class Champion
    {
        public string version { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string blurb { get; set; }
        public Info info { get; set; }
        public Image image { get; set; }
        public string[] tags { get; set; }
        public string partype { get; set; }
        public Stats stats { get; set; }
    }

    public class Info
    {
        public int attack { get; set; }
        public int defense { get; set; }
        public int magic { get; set; }
        public int difficulty { get; set; }
    }

    public class Image
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Stats
    {
        public int hp { get; set; }
        public int hpperlevel { get; set; }
        public int mp { get; set; }
        public int mpperlevel { get; set; }
        public int movespeed { get; set; }
        public int armor { get; set; }
        public float armorperlevel { get; set; }
        public float spellblock { get; set; }
        public float spellblockperlevel { get; set; }
        public int attackrange { get; set; }
        public int hpregen { get; set; }
        public int hpregenperlevel { get; set; }
        public int mpregen { get; set; }
        public int mpregenperlevel { get; set; }
        public int crit { get; set; }
        public int critperlevel { get; set; }
        public int attackdamage { get; set; }
        public int attackdamageperlevel { get; set; }
        public float attackspeedperlevel { get; set; }
        public float attackspeed { get; set; }
    }
}