﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorWho.Db.Migrations
{
    
    public partial class AddGetEnemiesFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"CREATE FUNCTION GetEnemies (@EpisodeId INT)
            RETURNS VARCHAR(255)
            AS
            BEGIN
            DECLARE @EnemiesList VARCHAR(255) = '';

            SELECT @EnemiesList = @EnemiesList + e.EnemyName + ', '
            FROM Enemies e
            JOIN EnemyEpisode ee ON e.EnemyId = ee.EnemiesEnemyId
            WHERE ee.EpisodesEpisodeId = @EpisodeId;

            RETURN @EnemiesList;
            END");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"DROP FUNCTION GetEnemies");
        }

    }
}
