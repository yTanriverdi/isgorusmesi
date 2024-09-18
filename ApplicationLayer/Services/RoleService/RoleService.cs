﻿using DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.RoleService
{
    public class RoleService : IRoleService
    {
       
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Tüm Role leri döndürür.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AppRole>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
            {
                return Enumerable.Empty<AppRole>();
            }
            else
            {
                return roles;
            }

        }

        /// <summary>
        ///  Yeni role ismiyle role oluşturma.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return false;
            }
            var role = new AppRole() 
            { 
                Name = roleName,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        /// <summary>
        /// id ve yeni role ismiyle güncelleme.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="newRoleName"></param>
        /// <returns></returns>

        public async Task<bool> UpdateRoleAsync(int roleId, string newRoleName)
        {
            if (roleId <= 0 || string.IsNullOrEmpty(newRoleName))
            {
                return false;
            }
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            role.Name = newRoleName;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        /// <summary>
        /// id ye ait role siler.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            if (roleId <= 0)
            {
                return false;
            }
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        /// <summary>
        /// id ye ait AppRole getirir.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<AppRole> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return null;
            else return role;
        }
    }
}
