using VelvetRelics.Models;

namespace VelvetRelics.Services;

/// <summary>
/// Реалізація сервісу товарів з інтегрованими преміальними Mock-даними для візуальної досконалості додатка.
/// Також підготовлено основу під роботу з HttpClient для майбутньої REST API інтеграції.
/// </summary>
public class ProductService : IProductService
{
    private readonly List<Product> _mockProducts;

    public ProductService()
    {
        // Створюємо преміальну колекцію антикваріату (мінімум по 2 предмети на кожну з 6 категорій)
        _mockProducts = new List<Product>
        {
            // ─── КАРТИНИ ────────────────────────────────────────────────────────
            new Product
            {
                Id = 1,
                Name = "Золота Осінь у Лісі",
                Description = "Оригінальне полотно невідомого майстра віденської школи. Неймовірна гра світла й тіні, глибокі масляні мазки та оригінальна позолочена рама з дуба XVII століття.",
                Year = "1874",
                Category = "картини",
                Price = 12500,
                ImageUrl = "https://images.unsplash.com/photo-1579783900882-c0d3dad7b119?w=600&q=80"
            },
            new Product
            {
                Id = 2,
                Name = "Портрет Невідомої в Оксамиті",
                Description = "Вишуканий італійський портрет епохи пізнього ренесансу. Тонка деталізація оксамитової сукні та перлинного намиста. Картина пройшла повну професійну реставрацію в Римі.",
                Year = "1689",
                Category = "картини",
                Price = 28000,
                ImageUrl = "https://images.unsplash.com/photo-1579783928621-7a13d66a62d1?w=600&q=80"
            },

            // ─── СТАРОВИННІ КНИГИ ────────────────────────────────────────────────
            new Product
            {
                Id = 3,
                Name = "Трактат про Алхімію та Метали",
                Description = "Шкіряна палітурка з мідними застібками. Рідкісне латинське видання з детальними гравюрами та ручними замітками на полях від попередніх власників.",
                Year = "1592",
                Category = "старовинні книги",
                Price = 8400,
                ImageUrl = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=600&q=80"
            },
            new Product
            {
                Id = 4,
                Name = "Поеми лицарської епохи (Рукопис)",
                Description = "Французький рукопис на тонкому пергаменті, прикрашений вишуканими кольоровими ініціалами та золотим напиленням. Справжній шедевр середньовічної каліграфії.",
                Year = "1430",
                Category = "старовинні книги",
                Price = 45000,
                ImageUrl = "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=600&q=80"
            },

            // ─── МОНЕТИ ─────────────────────────────────────────────────────────
            new Product
            {
                Id = 5,
                Name = "Золотий Солід Юстиніана I",
                Description = "Візантійська золота монета чудової збереженості (Grade: EF). На аверсі зображено імператора в шоломі з хрестом, на реверсі — Вікторію з посохом.",
                Year = "527-565",
                Category = "монети",
                Price = 3200,
                ImageUrl = "https://images.unsplash.com/photo-1621972750749-0fbb1abb7736?w=600&q=80"
            },
            new Product
            {
                Id = 6,
                Name = "Срібний Талер Марії Терезії",
                Description = "Великий срібний талер Австрійської Імперії. Справжній монетний блиск із красивою кабінетною патиною. Один із символів європейської торгівлі XVIII століття.",
                Year = "1780",
                Category = "монети",
                Price = 750,
                ImageUrl = "https://images.unsplash.com/photo-1607603750909-408e19385117?w=600&q=80"
            },

            // ─── МЕБЛІ ──────────────────────────────────────────────────────────
            new Product
            {
                Id = 7,
                Name = "Оксамитове Крісло Людовика XV",
                Description = "Королівське крісло з різьбленого горіха, оббите глибоким бордовим оксамитом з золотим кантом. Витончені вигнуті ніжки типу 'кабріоль'. Франція.",
                Year = "1750",
                Category = "меблі",
                Price = 9800,
                ImageUrl = "https://images.unsplash.com/photo-1595428774223-ef52624120d2?w=600&q=80"
            },
            new Product
            {
                Id = 8,
                Name = "Секретер з чорного дерева та мармуру",
                Description = "Великий кабінетний секретер із безліччю секретних шухляд. Оздоблений інкрустацією з перламутру, латуні та дорогоцінних порід дерева. Топ із чорного бельгійського мармуру.",
                Year = "1820",
                Category = "меблі",
                Price = 16500,
                ImageUrl = "https://images.unsplash.com/photo-1540518614846-7eded433c457?w=600&q=80"
            },

            // ─── ПРИКРАСИ ───────────────────────────────────────────────────────
            new Product
            {
                Id = 9,
                Name = "Перстень із Глибоким Смарагдом",
                Description = "Масивне вікторіанське золото 18K. У центрі — натуральний колумбійський смарагд класичного смарагдового огранювання вагою 2.4 карата, оточений дрібними діамантами.",
                Year = "1890",
                Category = "прикраси",
                Price = 14000,
                ImageUrl = "https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f?w=600&q=80"
            },
            new Product
            {
                Id = 10,
                Name = "Намисто з Королівським Бурштином",
                Description = "Унікальне намисто з великих пресованих та полірованих шматків рідкісного балтійського бурштину молочно-жовтого відтінку. Фурнітура зі срібла 925 проби.",
                Year = "1910",
                Category = "прикраси",
                Price = 2900,
                ImageUrl = "https://images.unsplash.com/photo-1535632066927-ab7c9ab60908?w=600&q=80"
            },

            // ─── ГОДИННИКИ ──────────────────────────────────────────────────────
            new Product
            {
                Id = 11,
                Name = "Золотий Кишеньковий Годинник Patek Philippe",
                Description = "Анкерний механізм на 18 рубінах, корпус із триколірного золота 14K із ручним гільошуванням та квітковим гравюрам. Годинник повністю обслужений та на точному ходу.",
                Year = "1905",
                Category = "годинники",
                Price = 22000,
                ImageUrl = "https://images.unsplash.com/photo-1509048191080-d2984bad6ae5?w=600&q=80"
            },
            new Product
            {
                Id = 12,
                Name = "Бронзовий Камінний Годинник 'Амур'",
                Description = "Розкішний французький камінний годинник з позолоченої патинованої бронзи. Сюжетна композиція з фігурою Амура. Маятниковий механізм із боєм кожні півгодини.",
                Year = "1840",
                Category = "годинники",
                Price = 7200,
                ImageUrl = "https://images.unsplash.com/photo-1524592094714-0f0654e20314?w=600&q=80"
            }
        };
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        // Симулюємо невелику затримку мережі (як у реальному інтернет-запиті)
        await Task.Delay(800);
        return _mockProducts;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        await Task.Delay(600);
        if (string.IsNullOrWhiteSpace(category))
            return _mockProducts;

        return _mockProducts.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await Task.Delay(400);
        return _mockProducts.FirstOrDefault(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
    {
        await Task.Delay(600);
        if (string.IsNullOrWhiteSpace(query))
            return _mockProducts;

        string lowerQuery = query.ToLower();
        return _mockProducts.Where(p => 
            p.Name.ToLower().Contains(lowerQuery) || 
            p.Description.ToLower().Contains(lowerQuery) ||
            p.Category.ToLower().Contains(lowerQuery));
    }
}
