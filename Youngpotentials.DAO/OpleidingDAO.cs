using System;
using System.Collections.Generic;
using System.Text;
using Youngpotentials.Domain.Entities;

namespace Youngpotentials.DAO
{
    public interface IOpleidingDAO
    {
        IEnumerable<Opleiding> GetAll();
        Opleiding GetById(string id);
    }
    public class OpleidingDAO
    {
    }
}
