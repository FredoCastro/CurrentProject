using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTOEntidades
{
    public class PlayerDTO : IPlayerDTO {

        private int? _id;

        public int? Id { get { return _id; }
            set {
                if (value == null)
                    throw new ArgumentNullException("Id");
                else
                    _id = value;
            }
        }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }

        //Relacion 1-many con Team
        public virtual TeamDTO Team { get; set; }
    }
}