using Exemple.Domain.Models;
using LanguageExt;
using System.Collections.Generic;
//using static Exemple.Domain.Models.ExamGrades;

namespace Exemple.Domain.Repositories
{
    public interface IProductsRepository
    {
        TryAsync<List<CalculatedPrice>> TryGetExistingProduct();

        //TryAsync<Unit> TrySaveProduct(PublishedExamGrades grades);
    }
}
