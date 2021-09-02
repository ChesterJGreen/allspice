using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class RecipesRepository
  {
      private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> Get()
    {
      string sql = @"
      SELECT 
      a.*,
      r.*
      FROM recipes r
      JOIN accounts a ON r.creatorId = a.id
      ";
      return _db.Query<Profile, Recipe, Recipe>(sql, (profile, recipes) =>
      {
          recipes.Creator = profile;
          return recipes;
      }, splitOn: "id").ToList();
    }
  }
}