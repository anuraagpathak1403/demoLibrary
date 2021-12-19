using demoLibrary.Data;
using demoLibrary.DTO;
using demoLibrary.Entities;
using demoLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace demoLibrary.Controllers
{
    [RoutePrefix("api/user")]
    public class userController : ApiController
    {
        public readonly dataContext _context;
        public readonly userRepository _userRepository;
        public userController(dataContext context, userRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("getUser")]
        public async Task<registration> getUser(string enrollmentId)
        {
            var user = await _userRepository.getUserDTOAsync(enrollmentId);
            return (registration)user;
        }
    }
}