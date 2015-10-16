using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;
using ByA;


namespace TestConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {

            DateTime fecha_1 = new DateTime(2016,07,01);
            DateTime fecha_2 = new DateTime(2016, 07, 31);

            Console.WriteLine(fecha_1 > fecha_2);
            Console.WriteLine(fecha_1 == fecha_2);
            Console.WriteLine(fecha_1 < fecha_2);


            /*using (ieEntities db = new ieEntities())
            {
                //&& t.vigencia == 2016 && t.estado == "PR" &&

                var lst = db.carterap.
                    Where(t =>  (t.valor - t.pagado) > 0 )
                        .GroupBy(t => new { t.estudiantes,t.vigencias, t.conceptos })
                        .Select(
                                x => new
                                {
                                    NombreEstudiante = x.Key.estudiantes.terceros.nombre,
                                    NombreConcepto = x.Key.conceptos.nombre,
                                    Vigencia= x.Key.vigencias.vigencia,
                                    Valor = x.Sum(t => t.valor),
                                    Pagado = x.Sum(t => t.pagado),
                                    Saldo = x.Sum(t => (t.valor - t.pagado)),
                                    Cantidad= x.Count()
                                }
                        ).ToList();

                foreach (var item in lst)
                { 
                    Console.WriteLine(item.Vigencia+" "+ item.NombreEstudiante + " " + item.NombreConcepto + " " + item.Valor + " " + item.Pagado + " " + item.Saldo+" "+ item.Cantidad +" " + item.Valor/item.Cantidad  );
                }
                Console.ReadKey();
            }*/
            
            
        }

   
    }
}
