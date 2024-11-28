using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository repository)
        {
            _userRepository = repository;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAllAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Informe o id do usuário");
            }

            var user = await _userRepository.GetByIdAsync((int)id);
            if (user == null)
            {
                return BadRequest("Os detalhes do usuário não estão disponíveis");
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }


        //POST: Users/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,HourValue,AddDate,Active,Id")] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Enter required fields");

            if (ModelState.IsValid)
            {
                await _userRepository.AddAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest("Informe o id do usuário");

            var user = await _userRepository.GetByIdAsync((int)id);

            if (user == null)
                return BadRequest("Os detalhes do usuário não estão disponíveis");

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,HourValue,AddDate,Active,Id")] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Enter required fields");

            if (id != user.Id)
                return BadRequest("Informe o id do usuário");

            if (ModelState.IsValid)
            {
                await _userRepository.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Informe o id do usuário");
            }

            var user = await _userRepository.GetByIdAsync((int)id);
            if (user == null)
            {
                return BadRequest("Os detalhes do usuário não estão disponíveis");
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest("Informe o id do usuário");
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.RemoveAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int? id)
        {
            return _userRepository.GetByIdAsync(id) != null;
        }
    }
}
