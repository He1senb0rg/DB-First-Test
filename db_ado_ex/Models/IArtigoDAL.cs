using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace db_ado_ex.Models
{
    public interface IArtigoDAL
    {
        IEnumerable<Artigo> GetAllArtigos();
        void AddArtigo(Artigo artigo);
        void UpdateArtigo(Artigo artigo);
        Artigo GetArtigo(int? id);
        void DeleteArtigo(int? id);
    }
}
