using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PacientesController : Controller
    {
        private readonly BdHcuchContext _context;        



        public PacientesController(BdHcuchContext context) //Inyectar Propiedas al Controlador
        {
            _context = context;
        }
        





        public IActionResult Index()
        {
            //TPaciente paciente = new TPaciente();  //Instancio una variable a la Clase TPaciente para acceder a sus campos
            //paciente.Nombres = "Jean"; //Seteo un valor a la propiedad Nombres


            //PacienteGeneralViewModel model = new PacienteGeneralViewModel(); //Instancio una variable a la clase ViewModel para acceder a sus campos
            //model.Nombres = paciente.Nombres; //Seteo en la propiedad Nombres del ViewModel el valor de Paciente.Nombres
            //model.InformacionGeneral = "Codigo:001 - " + model.Nombres + " No Tiene Apellidos";



            List<PacienteGeneralViewModel> listaPacientes = _context.TPacientes
                                                            .Select(p => new PacienteGeneralViewModel
                                                            {

                                                                IdPaciente = p.IdPaciente,

                                                                Nombres = p.Nombres,
                                                                PrimerApellido = p.PrimerApellido,
                                                                SegundoApellido = p.SegundoApellido,
                                                                FechaNacimiento = p.FechaNacimiento,
                                                                InformacionGeneral = "Código: " + p.Nombres + " " + p.PrimerApellido + " " + p.SegundoApellido
                                                            }).ToList();
      
            return View(listaPacientes);
        }

        public IActionResult Eliminar(int id)
        {


            TPaciente opaciente = _context.TPacientes.FirstOrDefault(x => x.IdPaciente == id);  //Obtengo el registro
            if (opaciente != null)
            {
                _context.TPacientes.Remove(opaciente); //Elimino el registro
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Pacientes");
        }

        public IActionResult NuevoPaciente(int id)
        {
            //id tiene un valor, se invoco desde Editar
            //Si id es vacio o nulo se invocó desde Nueevo Paciente

            PacienteGeneralViewModel opacienteviewmodel = new PacienteGeneralViewModel();

            if (id > 0)
            {
                TPaciente pacienterecuperado = _context.TPacientes.FirstOrDefault(x => x.IdPaciente == id);
                if (pacienterecuperado != null)
                {
                    opacienteviewmodel.Nombres = pacienterecuperado.Nombres;
                    opacienteviewmodel.PrimerApellido = pacienterecuperado.PrimerApellido;
                    opacienteviewmodel.SegundoApellido = pacienterecuperado.SegundoApellido;
                    opacienteviewmodel.FechaNacimiento = pacienterecuperado.FechaNacimiento;
                    opacienteviewmodel.IdPaciente = pacienterecuperado.IdPaciente;
                }
            }

            return View(opacienteviewmodel);

        }
        public IActionResult Registrar(int idpaciente, int idpacientex,string nombresReg, string primerapellidoReg, string segundopellidoReg, DateTime fechanacimientoReg)
        {

            
            
            int registrosinsertados = 0;
            string Mensaje = "";

            //Validar si existe el registro.
            TPaciente pacienterecuperado = _context.TPacientes.FirstOrDefault(x => x.IdPaciente == idpaciente);
            if (pacienterecuperado == null) {
                TPaciente paciente = new TPaciente();
                paciente.Nombres = nombresReg;
                paciente.PrimerApellido = primerapellidoReg;
                paciente.SegundoApellido = segundopellidoReg;
                paciente.FechaNacimiento = fechanacimientoReg;

                _context.TPacientes.Add(paciente);
                registrosinsertados = _context.SaveChanges();
                if (registrosinsertados > 0)
                {
                    //Mensaje de Retorno

                    Mensaje = "Se registro Correctamente la informacion";

                }
            }
            else{

                //pacienterecuperado.IdPaciente = id;
                pacienterecuperado.Nombres = nombresReg;
                pacienterecuperado.PrimerApellido = primerapellidoReg;
                pacienterecuperado.SegundoApellido = segundopellidoReg;
                pacienterecuperado.FechaNacimiento = fechanacimientoReg;
                _context.TPacientes.Update(pacienterecuperado);
                _context.SaveChanges();


                Mensaje = "El paciente ya existe en la base de datos";
            }
            return RedirectToAction("NuevoPaciente", "Pacientes");
        }

        public IActionResult Editar(int id)
        {
            return RedirectToAction("NuevoPaciente", "Pacientes",new { id = id });
        }
    }
}
