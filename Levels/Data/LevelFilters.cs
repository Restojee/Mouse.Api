﻿using Microsoft.EntityFrameworkCore;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;

namespace Mouse.NET.Levels.Data;

public static class LevelRepositoryFilters
{
    public static IQueryable<LevelEntity> GetFilterByUserQuery(IQueryable<LevelEntity> query, int userId)
    {
        return query.Where(level => level.User.Id == userId);
    }
    
    public static IQueryable<LevelEntity> GetFilterByCompletedQuery(MouseDbContext context, IQueryable<LevelEntity> query, int userId, bool isCompleted)
    {
        if (isCompleted) {
            return query.Where(level => context.LevelCompleted.Any(
                completed => completed.User.Id == userId && completed.Level.Id == level.Id));
        }
        return query.Where(level => !context.LevelCompleted.Any(completed => completed.User.Id == userId && completed.Level.Id == level.Id));
    }
    
    public static IQueryable<LevelEntity> GetFilterByFavoriteQuery(MouseDbContext context, IQueryable<LevelEntity> query, int userId, bool isFavorite)
    {
        if (isFavorite) {
            return query.Where(level => context.LevelFavorites.Any(completed => completed.User.Id == userId && completed.Level.Id == level.Id));
        }
        return query.Where(level => !context.LevelFavorites.Any(completed => completed.User.Id == userId && completed.Level.Id == level.Id));
    }
    
    public static IQueryable<LevelEntity> GetFilterByTags(IQueryable<LevelEntity> query, long[] tagIds)
    {
        foreach (int tagId in tagIds)
        {
            query = query.Where(level => level.Tags.Any(tag => tag.Id == tagId));
        }
        return query;
    }
    
    public static IQueryable<LevelEntity> GetFilterByName(IQueryable<LevelEntity> query, string name)
    {
        return query.Where(map => EF.Functions.Like(map.Name, "%" + name + "%"));
    }
}