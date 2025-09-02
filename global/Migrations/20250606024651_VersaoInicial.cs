﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace global.Migrations
{
    /// <inheritdoc />
    public partial class VersaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOG_API",
                columns: table => new
                {
                    ID_LOG = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TIPO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ENDPOINT = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PAYLOAD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RESPOSTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOG_API", x => x.ID_LOG);
                });

            migrationBuilder.CreateTable(
                name: "PREVISAO_TEMPO",
                columns: table => new
                {
                    ID_PREVISAO_TEMPO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LOCALIZACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    TEMPERATURA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CONDICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CHUVA_MM = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    VENTO_KMH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UMIDADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PREVISAO_TEMPO", x => x.ID_PREVISAO_TEMPO);
                });

            migrationBuilder.CreateTable(
                name: "SENSORES",
                columns: table => new
                {
                    ID_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LOCALIZACAO_SENSOR = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ALCANCE_ALERTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DATA_INSTALACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSORES", x => x.ID_SENSOR);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LOCALIZACAO_USUARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    RECEBER_ALERTAS = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "LEITURA_SENSOR",
                columns: table => new
                {
                    ID_LEITURA_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NIVEL_AGUA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEITURA_SENSOR", x => x.ID_LEITURA_SENSOR);
                    table.ForeignKey(
                        name: "FK_LEITURA_SENSOR_SENSORES_ID_SENSOR",
                        column: x => x.ID_SENSOR,
                        principalTable: "SENSORES",
                        principalColumn: "ID_SENSOR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ALERTA",
                columns: table => new
                {
                    ID_ALERTA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ID_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ID_PREVISAO_TEMPO = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TIPO_ALERTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MENSAGEM = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DATA_HORA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ENVIADO = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALERTA", x => x.ID_ALERTA);
                    table.ForeignKey(
                        name: "FK_ALERTA_PREVISAO_TEMPO_ID_PREVISAO_TEMPO",
                        column: x => x.ID_PREVISAO_TEMPO,
                        principalTable: "PREVISAO_TEMPO",
                        principalColumn: "ID_PREVISAO_TEMPO");
                    table.ForeignKey(
                        name: "FK_ALERTA_SENSORES_ID_SENSOR",
                        column: x => x.ID_SENSOR,
                        principalTable: "SENSORES",
                        principalColumn: "ID_SENSOR");
                    table.ForeignKey(
                        name: "FK_ALERTA_USUARIOS_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIOS",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALERTA_ID_PREVISAO_TEMPO",
                table: "ALERTA",
                column: "ID_PREVISAO_TEMPO");

            migrationBuilder.CreateIndex(
                name: "IX_ALERTA_ID_SENSOR",
                table: "ALERTA",
                column: "ID_SENSOR");

            migrationBuilder.CreateIndex(
                name: "IX_ALERTA_ID_USUARIO",
                table: "ALERTA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_LEITURA_SENSOR_ID_SENSOR",
                table: "LEITURA_SENSOR",
                column: "ID_SENSOR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALERTA");

            migrationBuilder.DropTable(
                name: "LEITURA_SENSOR");

            migrationBuilder.DropTable(
                name: "LOG_API");

            migrationBuilder.DropTable(
                name: "PREVISAO_TEMPO");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "SENSORES");
        }
    }
}
