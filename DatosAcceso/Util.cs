using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DatosAcceso {
    public class Util {

        public static string URICompetition() {
            string URICompetition = System.Configuration.ConfigurationManager.AppSettings["URICompetition"];
            if (URICompetition != null)
                return URICompetition;
            else
                throw new Exception("No se encuentra configurada la URI para Competición");
        }
        public static string URICTeam() {
            string URICTeam = System.Configuration.ConfigurationManager.AppSettings["URICTeam"];
            if (URICTeam != null)
                return URICTeam;
            else
                throw new Exception("No se encuentra configurada la URI para Equipos");
        }
        public static string URIPlayer() {
            string URIPlayer = System.Configuration.ConfigurationManager.AppSettings["URIPlayer"];
            if (URIPlayer != null)
                return URIPlayer;
            else
                throw new Exception("No se encuentra configurada la URI para Jugadores");
        }
        public static string APITokenName() {
            string APITokenName = System.Configuration.ConfigurationManager.AppSettings["APITokenName"];
            if (APITokenName != null)
                return APITokenName;
            else
                throw new Exception("No se encuentra configurada el nombre del Token de la API futbol");
        }

        public static string APITokenValue() {
            string APITokenValue = System.Configuration.ConfigurationManager.AppSettings["APITokenValue"];
            if (APITokenValue != null)
                return APITokenValue;
            else
                throw new Exception("No se encuentra configurada el valor del Token de la API futbol");
        }
    }
}