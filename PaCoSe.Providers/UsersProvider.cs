using AutoMapper;
using PaCoSe.Caching;
using PaCoSe.Contracts;
using PaCoSe.Core.Extensions;
using PaCoSe.Exceptions;
using PaCoSe.Infra.Persistence;
using PaCoSe.Models;
using System;
using System.Collections.Generic;

namespace PaCoSe.Providers
{
    public class UsersProvider : BaseProvider, IUsersContract
    {
        public UsersProvider(ICacheProvider cacheProvider, IMapper mapper, IAppDatabase appDatabase) : base(cacheProvider, mapper, appDatabase)
        {
        }

        public User AddUser(User user)
        {
            var existingUser = this.GetUserByUsername(user.Username);
            if (existingUser == null)
            {
                var existingUserProfile = this.GetUserProfileByEmail(user.UserProfile.Email);
                if (existingUserProfile != null)
                {
                    throw new DuplicateEntryException($"A user with the given email already exists.");
                }
            }
            else
            {
                throw new DuplicateEntryException($"A user with the given username already exists.");
            }

            try
            {
                var userDataModel = this.Mapper.MapTo<Data.Model.User>(user);
                var userProfileDataModel = this.Mapper.MapTo<Data.Model.UserProfile>(user.UserProfile);

                this.Database.BeginTransaction();
                user.Id = this.Database.Insert(userDataModel);
                userProfileDataModel.UserId = user.UserProfile.UserId = user.Id;
                user.UserProfile.Id = this.Database.Insert(userProfileDataModel);
                this.Database.CompleteTransaction();

                return user;
            }
            catch (Exception e)
            {
                this.Database.AbortTransaction();
                throw new DatabaseException("An error occured while creating the user", e);
            }
        }

        public User AddUserWithoutProfile(User user)
        {
            var existingUser = this.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                throw new DuplicateEntryException($"A user with the given username already exists.");
            }

            user.IsActivated = user.IsInvited = false;
            var userDataModel = this.Mapper.MapTo<Data.Model.User>(user);
            user.Id = this.Database.Insert(userDataModel);
            return user;
        }

        public bool InviteUser(int userId)
        {
            return false; // TODO: Implement
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool DisableUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            var user = this.Database.FirstOrDefault<Data.Model.User>("WHERE id = @0", id);
            return this.Mapper.MapTo<User>(user);
        }

        public User GetUserByUsername(string username)
        {
            var user = this.Database.FirstOrDefault<Data.Model.User>("WHERE Username = @0", username);
            return this.Mapper.MapTo<User>(user);
        }

        public UserProfile GetUserProfileByEmail(string email)
        {
            var userProfile = this.Database.FirstOrDefault<Data.Model.UserProfile>("WHERE Email = @0", email);
            return this.Mapper.MapTo<UserProfile>(userProfile);
        }

        public User UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
