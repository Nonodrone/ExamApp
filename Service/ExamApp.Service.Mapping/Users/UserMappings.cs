using ExamApp.Data.Models;
using ExamApp.Service.Models.Users;

namespace ExamApp.Service.Mapping.Users
{
    public static class UserMappings
    {
        public static ExamAppUser ToEntity(this ExamAppUserDto examAppUserDto)
        {
            return new ExamAppUser
            {
                Id = examAppUserDto.Id,
                UserName = examAppUserDto.UserName,
                Email = examAppUserDto.Email,
            };
        }

        public static ExamAppUserDto ToDto(this ExamAppUser examAppUser)
        {
            return new ExamAppUserDto
            {
                Id = examAppUser.Id,
                UserName = examAppUser.UserName,
                Email = examAppUser.Email,
            };
        }
    }
}
