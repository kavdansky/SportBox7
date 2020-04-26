using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportBox7.Data.Seed
{
    public static class DBInitializer
    {

        public static void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IApplicationBuilder app)
        {
            SeedRoles(roleManager);
            Thread.Sleep(1000);
            SeedUsers(userManager);
            Thread.Sleep(1000);
            SeedCategoties(app);
            Thread.Sleep(1000);
            SeedUserPermitedCategoties(app);
            Thread.Sleep(1000);
            SeedArticles(app);
            Thread.Sleep(1000);
            SeedArticlesSeoData(app);
        }

        private static void SeedArticlesSeoData(IApplicationBuilder app)
        {
            using var serviceScope = app?.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var articles = context.Articles.ToArray();
            if (context.ArticlesSeoData.ToArray().Length == 0)
            {
                foreach (var article in articles)
                {
                    ArticleSeoData articleSeoDataTenp = new ArticleSeoData { ArticleId = article.Id, MetaDescription = article.Title, MetaKeyword = article.Title, SeoUrl = article.SourceURL, MetaTitle = article.Title };
                    context.ArticlesSeoData.Add(articleSeoDataTenp);
                }
                context.SaveChanges();
            }
            
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager?.FindByNameAsync("kavdansky@mail.bg").Result == null)
            {
                User user = new User();
                user.UserName = "kavdansky@mail.bg";
                user.Email = "kavdansky@mail.bg";
                user.EmailConfirmed = true;
                user.IsActive = true;
                IdentityResult result = userManager.CreateAsync(user, "Kavdansky1!").Result;
                
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"Admin").Wait();
                }
            }


            if (userManager?.FindByNameAsync("rusulski@mail.bg").Result == null)
            {
                User user = new User();
                user.UserName = "rusulski@mail.bg";
                user.Email = "rusulski@mail.bg";
                user.EmailConfirmed = true;
                user.IsActive = true;
                IdentityResult result = userManager.CreateAsync
                (user, "Rusulski1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "ChiefEditor").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("ChiefEditor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "ChiefEditor";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Author").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Author";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedCategoties(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder?.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context.Categories?.Count() < 1)
            {
                Category category1 = new Category();
                category1.CategoryName = "Футбол БГ";
                category1.CategoryNameEN = "FootballBG";
                category1.CategoryNameSportsDb = "Soccer";
                context.Add(category1);

                Category category2 = new Category();
                category2.CategoryName = "Футбол свят";
                category2.CategoryNameEN = "FootballWorld";
                category2.CategoryNameSportsDb = "Soccer";
                context.Add(category2);

                Category category3 = new Category();
                category3.CategoryName = "Баскетбол";
                category3.CategoryNameEN = "Baskeball";
                category3.CategoryNameSportsDb = "Basketball";
                context.Add(category3);

                Category category4 = new Category();
                category4.CategoryName = "Волейбол";
                category4.CategoryNameEN = "Valleyball";
                category4.CategoryNameSportsDb = "Volleyball";
                context.Add(category4);

                Category category5 = new Category();
                category5.CategoryName = "Бойни";
                category5.CategoryNameEN = "MartialArts";
                category5.CategoryNameSportsDb = "Fighting";
                context.Add(category5);

                Category category6 = new Category();
                category6.CategoryName = "Други спортове";
                category6.CategoryNameEN = "Others";
                context.Add(category6);

                context.SaveChanges();
            }

        }

        public static void SeedUserPermitedCategoties(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder?.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context.UserCategories?.Count() < 1)
            {


                var adminUser = context.Users.Where(x => x.Email == "kavdansky@mail.bg").FirstOrDefault();
                var chiefEditor = context.Users.Where(x => x.Email == "rusulski@mail.bg").FirstOrDefault();
                var categories = context.Categories.ToList();

                
                for (int p = 0; p < categories.Count; p++)
                {
                    UserCategory userCat = new UserCategory();
                    userCat.UserId = adminUser.Id;
                    userCat.CategoryId = categories[p].Id;
                    context.UserCategories.Add(userCat);
                    
                }
                for (int p = 0; p < categories.Count; p++)
                {
                    UserCategory userCat = new UserCategory();
                    userCat.UserId = chiefEditor.Id;
                    userCat.CategoryId = categories[p].Id;
                    context.UserCategories.Add(userCat);

                }
                context.SaveChanges();               
            }
        }
        public static void SeedArticles(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder?.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var adminUser = context.Users.Where(x => x.Email == "kavdansky@mail.bg").FirstOrDefault();

            var artcles = context.Articles.ToList();
            if (artcles.Count == 0)
            {
                Article article1 = new Article()
                {
                    Title = "Честит юбилей на Георги Денев",
                    Body = "Днес 70-и юбилей празнува знаменитият български футболист Георги Денев. Бившият национал е роден на 18 април 1950 г. в Ловеч, а кариерата му стартира в местния Кърпачев. През сезон 1968/69 играе за Спартак (Плевен), а след това преминава в ЦСКА. За \"армейците\" играе цяло десетилетие, като записва 237 мача и 78 гола за първенство, пет пъти печели шампионската титла и още три пъти Купата на България. В края на кариерата си излиза в чужбина и последователно облича екипите на Етникос (42 мача, 18 гола) и Арис (Лимасол, 47 мача, 16 гола).За националния отбор Денев дебютира през 1970 г.и изиграва 49 мача, в които се разписва десет пъти.Участник на световното първенство във ФРГ през 1974 - а.Българският футболен съюз честити празника на Георги Денев и му желае здраве, късмет и дълголетие!",
                    H1Tag = "Честит юбилей на Георги Денев",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-18/size1/GDenev.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46795/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 3, 18),
                    CreationDate = new DateTime(2020, 3, 21),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 1,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article2 = new Article()
                {
                    Title = "Честит рожден ден на Георги Терзиев",
                    Body = "Днес 28 години навършва българският национал Георги Терзиев. Защитникът е роден на 18 април 1992 г. в Сливен, а футболната му кариера започва от школата на едноименния местен клуб. Дебюта си в професионалния футбол Терзиев прави с екипа на Нафтекс на едва 15-годишна възраст, а между 2009-а и 2013-а играе за другия бургаски тим - Черноморец (79 мача, 4 гола). Следва трансфер в Лудогорец, за който до момента бранителят има 143 мача и 5 попадения във всички турнири. Шесткратен шампион на страната, носител на Купата на България, трикратен победител в турнира за Суперкупата. През пролетта на сезон 2016/17 Терзиев играе под наем в хърватския гранд Хайдук (Сплит), записвайки 8 мача за първенство. За националния отбор защитникът дебютира на 7 октомври 2011 г.при загубата от Украйна в приятелски мач, а към днешна дата има 14 изиграни срещи за \"лъвовете\".Българският футболен съюз честити празника на Георги Терзиев и му желае здраве и още много футболни успехи!",
                    H1Tag = "Честит рожден ден на Георги Терзиев",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-18/size1/GTerziev.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46794/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 3, 18),
                    CreationDate = new DateTime(2020, 3, 21),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 2,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article3 = new Article()
                {
                    Title = "Честит рожден ден на Кирил Домусчиев",
                    Body = "Днес 51 години навършва членът на Изпълнителния комитет на Българския футболен съюз Кирил Домусчиев. Собственикът на футболен клуб Лудогорец е роден на 18 април 1969 г. в София. Магистър по Индустриален мениджмънт и маркетинг от Техническия университет, основател на „Напредък Холдинг” АД (чрез „Кимаго” ООД), председател на Надзорния съвет на “Биовет” АД; изпълнителен директор на “Хювефарма” АД. Собственик на 50 % of “Адванс Пропъртис” ООД. Член на Борда на Директорите на “Huvepharma” N.V., Белгия, член на Борда на Директорите на “Huvepharma” Inc., САЩ.  Председател на Борда на Директорите на “Кей Джи Маритайм Шипинг” АД, председател на Борда на Директорите на “ Кей Джи Маритайм Партнърс” АД. Главен акционер в „Параходство Български Морски Флот” АД, председател на Надзорния съвет на „Параходство БМФ” АД. Председател на Конфедерацията на работодателите и индустриалците в България. Във футболните среди Кирил Домусчиев е познат като основоположник напроекта \"Лудогорец\", извел разградския клуб доосем шампионски титли и две Купи на България, както и запомнящи се участия в европейските клубни турнири.Българският футболен съюз честити празникана г - н Домусчиев и му желае здраве и късмет!",
                    H1Tag = "Честит рожден ден на Кирил Домусчиев",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-18/size1/image_1516897817_19437.jpeg",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46793/0",
                    SourceName = "Български баскетболен съюз",
                    LastModDate = new DateTime(2020, 4, 1),
                    CreationDate = new DateTime(2020, 4, 1),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 3,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article4 = new Article()
                {
                    Title = "Честит рожден ден на Антон Попов",
                    Body = "Днес 63 години навършва заместник-изпълнителният директор на Българския футболен съюз Антон Попов. Той е роден на 16 април 1957 г. в град Горна Оряховица, завършва НСА \"Васил Левски\" със специалности \"Педагогика\" и \"Футбол\". Бивш Главен секретар в Държавната агенция за младежта и спорта, както и председател на Областния съвет на БФС в Стара Загора. От февруари 2014 г. е на поста заместник-изпълнителен директор на БФС. Ръководството и служителите на Българския футболен съюз желаят на Антон Поповздравеи успехи както в професионален, така и в личен план!",
                    H1Tag = "Честит рожден ден на Антон Попов",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-16/size1/image_1523877794_10219.jpeg",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46792/0",
                    SourceName = "Български волейболен съюз",
                    LastModDate = new DateTime(2020, 4, 5),
                    CreationDate = new DateTime(2020, 4, 5),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 4,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article5 = new Article()
                {
                    Title = "Честит рожден ден на Галин Иванов",
                    Body = "Днес 32 години навършва българският национал Галин Иванов. Полузащитникът е роден на 15 април 1988 г. в Казанлък и започва да се занимава с футбол в школата на местния Розова долина. Впоследствие продължава пътя си в ДЮШ на старозагорския Траяна, а дебютът му в професионалния спорт е с екипа на Левски (София). Сезон 2006/07 прекарва в Берое, а най-дълго се задържа в столичния Славия. В първия си период на \"Овча купел\" Иванов изиграва 128 мача и бележи 25 гола за първенство, като на два пъти е отдаван под наем - веднъж на немския Арминия (Билефелд) и веднъж на Литекс. През 2014-а осъществява трансфер в азербайджанския Хазар, след което играе за турския Самсунспор. През 2016/17 облича екипите на Левски и Нефтохимик, следва сезон и половина в Славия (със спечелена Купа на България), кратък престой в унгарския Халадаш (13 мача, 2 гола) и ново завръщане в Славия. За националния отбор Галин Иванов дебютира през 2016 - а по време на турнира \"Кирин къп\", а до момента има десет изиграни мача и един отбелязан гол - срещу Словения в турнира Лига на нациите. Българският футболен съюз честити рождения ден на Галин Иванов и му желае здраве, късмет и футболно дълголетие!",
                    H1Tag = "Честит рожден ден на Галин Иванов",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-16/size1/GalIvanov.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46791/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 4, 6),
                    CreationDate = new DateTime(2020, 4, 7),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 5,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article6 = new Article()
                {
                    Title = "Планираният брифинг на ръководството на БФС ще се състои на 21 април",
                    Body = "В следствие на епидемиологичната обстановка в страната и с оглед предстоящите Великденски празници, планираното за 17 април (петък) заседание на Изпълнителния комитет на Българския футболен съюз бе пренасрочено за вторник, 21 април, от 14:00 часа. По същите причини обявеният за утре, 16 април, брифинг за медиите, ще се проведе непосредствено след приключване на заседанието на Изпълкома на 21 април, при задължителнитемерки за безопасност иограничаване разпространението на заразата от COVID - 19.",
                    H1Tag = "Планираният брифинг на ръководството на БФС ще се състои на 21 април ",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-15/size1/BFS%20BG%20GRADIENT%20copy.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46790/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 4, 8),
                    CreationDate = new DateTime(2020, 4, 10),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 6,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article7 = new Article()
                {
                    Title = "Продължава благородната инициатива на феновете на \"Локомотив\" (Пловдив) в помощ на нуждаещите се граждани",
                    Body = "В навечерието на Великден и в условието на извънредно положение продължава благородната инициатива на Клуба на привържениците на „Локомотив” (Пловдив), чийто членове помагат безкористно и безвъзмездно на възрастни, бедни и безпомощни жители на града, като разнасят по домовете им храна и продукти от първа необходимост. За благородната цел запалянковцитеизползват собствен транспорт, който се дезинфекцира преди всяко разнасяне на храна, а сборният пункт е стадион „Локомотив” в парка „Лаута”. Възрастните хора предават по телефона списък с желаните продукти на стойност не повече от 40 лв.(за да могатда бъдат изпълнени по - голям брой заявки), феновете закупуват продуктитеи ги разнасят по домовете, като представят касовите бележки.Всички участници в акциитеса с маски и ръкавици, като преди тръгване им се мери температурата.Със средства, изпратени от привърженици зад граница, се закупуват маски, дезинфектанти и ръкавици, които се добавят към пакетите, а само през последния уикенд пет екипа доброволци са посетили 50 адреса в града.Феновете доставят стоките от първа необходимост през уикендите, но при спешни случаи се отзовават и през седмицата, катотранспортът е използван и от дарители, както и от БЧК при снабдяване с продукти за хора в нужда. По случай предстоящиясветъл празник е обърнато специално внимание на онези служители от клуба, които - макар на втори план - са неизменна част от него в продължение на десетилетия.Хората на портала, служителите за поддръжка на терена, охраната на футболния комплекс ишофьорът на клубния автобус са получили по един тематичен черно - бял Великденски пакет с посланието, че публика, отбор, ръководство и служители, включително и бивши, са остават заедно и в най - тежките ситуации.По време на доставките за привържениците на „Локомотив” пък е имало специални изненади - фланелки и блузи с логото на фенклуба. Клубът на привържениците на „Локомотив” призовава всички хора да бъдат по - отговорни и дисциплинирани при изпълнение на подчертаните от Националния оперативен щаб мерки, да бъдат по - толерантни и да се отнасятс разбиране към хората на предна линия в борбата срещуCOVID 19 – лекари, фармацевти, продавачи в магазини и бензиностанции; да бъдат грижовни към възрастните и безпомощните. След излъчен по БНТ репортаж за инициативата на запалянковците, с тях са се свързали от Димитровград за обмяна на опит в обгрижване на възрастни хора от града.  Набирането на доброволци продължава и всеки би могъл да се присъедини - телефоните за контакт и за приемане на поръчки за срока на извънредното положение могат да бъдат открити в постовете на официалните Интернет профили на фен-клуба в социалните мрежи. Българският футболен съюз изказва възхищение и подкрепа към действията на привържениците на \"Локомотив\"(Пловдив) и всички останали организирани групи фенове, демонстриращи социално отговорно поведение в условията на извънредно положение. Вярваме, че подобни добри примери са нагледна демонстрация на факта, че футболът е социален феномен, който обединява, а обществото ни може сплотено да премине през тези трудни времена.",
                    H1Tag = "Продължава благородната инициатива на феновете на \"Локомотив\" (Пловдив) в помощ на нуждаещите се граждани",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-14/size1/L1.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46788/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 4, 15),
                    CreationDate = new DateTime(2020, 4, 15),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 1,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article8 = new Article()
                {
                    Title = "Експерт от БФС проведе онлайн лекция в НСА по стратегически мениджмънт и управление на риска",
                    Body = "Експертът от Международния отдел на Българския футболен съюз Борис Станков проведе онлайн лекция пред студенти от НСА по стратегически мениджмънт и управление на риска в спорта. Станков, който наскоро се дипломира по програмата на УЕФА – DFLM, участва като гост-лектор по покана на доц. Иван Сандански от НСА. В уебинара взеха участие студенти по Спортен мениджмънт от 1-ви до 4-ти курс, които в момента следват бакалавърска и магистърска степен. Освен стратегически мениджмънт и мениджмънт на риска, сред темите на лекцията още бяха ефектът на кризата с коронавируса върху спортните институции / клубове, тяхната реакция и стратегическа визия за излизането от кризата, реакцията на ФИФА и УЕФА спрямо федерациите и препоръките към реакцията на европейските клубове, какви са предизвикателствата за договорните отношения между клубове и играчи, какви са юридическите пречки и др.Освен Борис Станков, участие в днешната лекция взе и проф.Йордан Иванов.",
                    H1Tag = "Експерт от БФС проведе онлайн лекция в НСА по стратегически мениджмънт и управление на риска",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-14/size1/BFS%20BG%20GRADIENT%20copy.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46789/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 4, 18),
                    CreationDate = new DateTime(2020, 4, 21),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 2,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article9 = new Article()
                {
                    Title = "Честит рожден ден на Велизар Димитров",
                    Body = "Днес 41 години навършва бившият български национал Велизар Димитров. Атакуващият халф е роден на 13 април 1979 г. в Перник, а футболната си кариера стартира от детско-юношеската школа на местния Миньор. За родния си клуб изиграва 28 мача за първенство, в които бележи 6 гола; през 2000-ата за кратко се подвизава в Локомотив (София), а сериозният му пробив идва с фланелката на дупнишкия Марек, за който записва 43 двубоя и 18 попадения. Добрите изяви му осигуряват трансфер в ЦСКА, където бързо се налага като основен футболист. В рамките на шест сезона изиграва 158 мача за \"червените\" във всички турнири, в които се разписва 45 пъти, на три пъти става шампион на страната и веднъж печели Купата на България. През лятото на 2008-а подписва с украинския Металург (Донецк), където завършва кариерата си, добавяйки 108 мача и 18 гола за първенство към визитката си. За националния отбор на България Велизар Димитров дебютира през 2004 - а и общо изиграва 31 мача, в които вкарва 4 гола.Участник на европейското първенство в Португалия през 2004 - а. След края на състезателната си кариера работи като скаут за Металург(Донецк) и ЦСКА - София. Българският футболен съюз честити рождения ден на Велизар Димитров и му желае късмет и много бъдещи успехи!",
                    H1Tag = "Честит рожден ден на Велизар Димитров",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-13/size1/VDimitrov.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46787/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 4, 12),
                    CreationDate = new DateTime(2020, 4, 12),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 3,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article10 = new Article()
                {
                    Title = "Честит рожден ден на Станислав Ангелов",
                    Body = "Днес 42-ия си рожден ден празнува бившият национал Станислав Ангелов. Мултифункционалният футболист е роден на 12 април 1978 г. в София, а първият му професионален клуб е ЦСКА, където играе три години и отбелязва един гол в 44 мача във всички турнири. През 2001-а преминава в редиците на вечния съперник Левски със свободен трансфер, като играе със синята фланелка в продължение на шест сезона (127 мача, 9 гола), преди да осъществи трансфер в Бундеслигата, подписвайки с Енерги (Котбус). Следват кратки престои в румънския гранд Стяуа и Анортозис (Кипър), а между 2012-а и 2014-а Ангелов отново се състезава за Левски. Трикратен шампион на страната с Левски, петкратен носител на Купата на България (веднъж с ЦСКА и четири пъти с Левски), двукратен носител на Суперкупата на България. За националния отбор Ангелов дебютира през 2006 - а и в следващите пет години записва 39 мача, в които се разписва веднъж. След края на състезателната си кариера работи като директор на детско - юношеската школа на Левски на стадион \"Раковски\".Българският футболен съюз честити празника на Станислав Ангелов и му желае здраве, щастие и успехи!",
                    H1Tag = "Честит рожден ден на Станислав Ангелов",
                    ImageUrl = "https://bfunion.bg/uploads/2020-04-12/size1/SAngelov.png",
                    SourceURL = "https://bfunion.bg/archive/2020/4/46786/0",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 4, 5),
                    CreationDate = new DateTime(2020, 4, 6),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 4,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false
                    

                };
                Article article11 = new Article()
                {
                    Title = "10 клуба искат край на сезона във волейбола",
                    Body = "Десет клуба от елита на българския волейбол се обявиха за прекратяване на Суперлигата. Според тях временното класиране трябва да бъде обявено за крайно, което означава титла за Нефтохимик 2010 (Бургас) при мъжете и за Марица (Пловдив) при жените. А въпросното искане се явява първият казус пред новия управителен съвет и Любо Ганев като президент на федерацията.",
                    H1Tag = "10 клуба искат край на сезона във волейбола",
                    ImageUrl = "https://www.volleyball.bg/images/ad599b9fcc57857fe9e8210e5336a36d_XL.jpg",
                    SourceURL = "https://www.volleyball.bg/news/item/6197",
                    SourceName = "Български футболен съюз",
                    LastModDate = new DateTime(2020, 4, 11),
                    CreationDate = new DateTime(2020, 4, 12),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 5,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };
                Article article12 = new Article()
                {
                    Title = "Любо Ганев свиква конферентен УС",
                    Body = "Любо Ганев свиква следващата седмица първо заседание на управителния съвет на волейболната федерация след проведеното на 13 март общо събрание. Новоизбраните членове на ръководството трябва да решат как ще завършат националните първенства.",
                    H1Tag = "Любо Ганев свиква конферентен УС",
                    ImageUrl = "https://www.volleyball.bg/media/k2/items/cache/ad599b9fcc57857fe9e8210e5336a36d_XL.jpg",
                    SourceURL = "https://www.volleyball.bg/news/item/6196",
                    SourceName = "Български съюз",
                    LastModDate = new DateTime(2020, 4, 5),
                    CreationDate = new DateTime(2020, 4, 5),
                    CreatorId = adminUser.Id,
                    EnableComments = true,
                    CategoryId = 6,
                    State = Enums.ArticleState.Published,
                    IsDeleted = false

                };

                article1.ArticleSeoData = new ArticleSeoData 
                {
                    MetaDescription = article1.Title,
                    MetaKeyword = article1.Title,
                    SeoUrl = article1.Title.Replace(" ", "-"),
                    MetaTitle = article1.Title 
                };
                article2.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article2.Title,
                    MetaKeyword = article2.Title,
                    SeoUrl = article2.Title.Replace(" ", "-"),
                    MetaTitle = article2.Title
                };
                article3.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article3.Title,
                    MetaKeyword = article3.Title,
                    SeoUrl = article3.Title.Replace(" ", "-"),
                    MetaTitle = article3.Title
                };
                article4.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article4.Title,
                    MetaKeyword = article4.Title,
                    SeoUrl = article4.Title.Replace(" ", "-"),
                    MetaTitle = article4.Title
                };
                article5.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article5.Title,
                    MetaKeyword = article5.Title,
                    SeoUrl = article5.Title.Replace(" ", "-"),
                    MetaTitle = article5.Title
                };
                article6.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article6.Title,
                    MetaKeyword = article6.Title,
                    SeoUrl = article6.Title.Replace(" ", "-"),
                    MetaTitle = article6.Title
                };
                article7.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article7.Title,
                    MetaKeyword = article7.Title,
                    SeoUrl = article7.Title.Replace(" ", "-"),
                    MetaTitle = article7.Title
                };
                article8.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article8.Title,
                    MetaKeyword = article8.Title,
                    SeoUrl = article8.Title.Replace(" ", "-"),
                    MetaTitle = article8.Title
                };
                article9.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article9.Title,
                    MetaKeyword = article9.Title,
                    SeoUrl = article9.Title.Replace(" ", "-"),
                    MetaTitle = article9.Title
                };
                article10.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article10.Title,
                    MetaKeyword = article10.Title,
                    SeoUrl = article10.Title.Replace(" ", "-"),
                    MetaTitle = article10.Title
                };
                article11.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article11.Title,
                    MetaKeyword = article11.Title,
                    SeoUrl = article11.Title.Replace(" ", "-"),
                    MetaTitle = article11.Title
                };
                article12.ArticleSeoData = new ArticleSeoData
                {
                    MetaDescription = article12.Title,
                    MetaKeyword = article12.Title,
                    SeoUrl = article12.Title.Replace(" ", "-"),
                    MetaTitle = article12.Title
                };


                context.Articles.Add(article1);
                context.Articles.Add(article2);
                context.Articles.Add(article3);
                context.Articles.Add(article4);
                context.Articles.Add(article5);
                context.Articles.Add(article6);
                context.Articles.Add(article7);
                context.Articles.Add(article8);
                context.Articles.Add(article9);
                context.Articles.Add(article10);
                context.Articles.Add(article11);
                context.Articles.Add(article12);
                context.SaveChanges();

            }

        }

    }
}
