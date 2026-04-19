namespace Tarea1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Taller 1");
            Console.WriteLine("\n");

            Console.WriteLine("Sistema educativo IUJO\n");

            //DATOS DEL USUARIO

            string registroUsuario = "    ID444   ; LauraVargas ;  Evaluacion   ;   95";

            string registroLimpio = registroUsuario.Trim();

            Console.WriteLine(registroLimpio);

            Console.WriteLine("\n");

            string[] partes = registroLimpio.Split(';');
            string id = partes[0].Trim();
            string nombre = partes[1].Trim();
            string tarea = partes[2].Trim();
            string nota = partes[3].Trim();

            Console.WriteLine(string.Format("{0} \n Usuario: {1} \n Nota: {2} \n Evaluacion: {3}", id, nombre, nota, tarea));

            Console.WriteLine("\n");

            //FLUJO DE ARCHIVOS

            string rutaraiz = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosIUJO");

            if (!Directory.Exists(rutaraiz))
            {
                Directory.CreateDirectory(rutaraiz);

                Console.WriteLine("Directorio Creado con exito");
            }

            string archivotexto = Path.Combine(rutaraiz, "notas.txt");

            Console.WriteLine("\n");
            Console.WriteLine(archivotexto);
            Console.WriteLine("\n");

            using (StreamWriter sw = new StreamWriter(archivotexto, true))
            {
                sw.WriteLine(string.Format("ID : {0} Nota {1} Fecha: {2}", id, nota, DateTime.Now.ToString("yyyy-MM-dd-HH:mm")));
            }

            //DESAFIO 1: El Validador de Seguridad

            Console.WriteLine("\n");
            Console.WriteLine("Desafio 1");

            string entradaSeguridad = "Laura;Clave123";
            string[] datosSeguridad = entradaSeguridad.Split(';');
            string clave = datosSeguridad[1];

            if (clave.Contains("123"))
            {
                using (StreamWriter swSeg = new StreamWriter(Path.Combine(rutaraiz, "seguridad.txt"), true))
                {
                    swSeg.WriteLine("CLAVE DEBIL:" + datosSeguridad[0]);
                }
                Console.WriteLine("¡Aviso de seguridad guardado!");
            }

            //DESAFIO 2: El clonador de imagenes (Filestream)

            Console.WriteLine("\n");
            Console.WriteLine("Desafio 2");

            string rutaOriginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "avatar.jpeg");
            string rutaCopia = Path.Combine(rutaraiz, "respaldo.jpeg");

            if (File.Exists(rutaOriginal))
            {
                using (FileStream Origen = new FileStream(rutaOriginal, FileMode.Open))
                using (FileStream destino = new FileStream(rutaCopia, FileMode.Create))
                {
                    byte[] buffer = new byte[1024];
                    int bytesLeidos;

                    while ((bytesLeidos = Origen.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destino.Write(buffer, 0, bytesLeidos);
                    }
                }
                Console.WriteLine("Imagen clonada con éxito en la carpeta" + rutaraiz);
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("No se encontró 'avatar.jpg' en la carpeta Debug.");
                Console.WriteLine("\n");
            }

            //DESAFIO 3: El Buscador de Archivos Pesados

            Console.WriteLine("\n");
            Console.WriteLine("Desafio 3");

            string[] todosLosArchivos = Directory.GetFiles(rutaraiz);

            foreach (string ruta in todosLosArchivos)
            {
                FileInfo info = new FileInfo(ruta);

                if (info.Length > 5120)
                {
                    string nombreArchivo = info.Name;
                    long tamaño = info.Length;
                    info.Delete();

                    Console.WriteLine("Archivo borrado por exceder los 5KB", nombreArchivo, tamaño);
                }
            }
            Console.WriteLine("Proceso de borrado completado.");
            Console.WriteLine("\n");

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
