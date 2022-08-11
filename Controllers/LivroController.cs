﻿using API_Livros.Models;
using API_Livros.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Livros.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroRepositorio _livroRepositorio;
        private ICsvParserService _csvParser;

        //Construtor da classe
        public LivroController(ILivroRepositorio livroRepositorio, ICsvParserService csvParser)
        {
            _livroRepositorio = livroRepositorio;
            _csvParser = csvParser;

        }
        //Métodos GET
        public IActionResult Index()
        {
            var livros = _livroRepositorio.BuscarTodos();
            return View(livros);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            //Recebendo informações
            LivroModel livro = _livroRepositorio.ListarPorId(id);
            return View(livro);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            //Recebendo informações
            LivroModel livro = _livroRepositorio.ListarPorId(id);
            return View(livro);
        }

        //Métodos POST
        [HttpPost]
        public IActionResult Criar(LivroModel p_livro) //Sobrecarga do método
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _livroRepositorio.Adicionar(p_livro);
                    TempData["MensagemSucesso"] = @"Livro adicionado com sucesso \(￣︶￣*\))";
                    return RedirectToAction("Index");
                }
                return View(p_livro);
            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $@"Erro ao adicionar o livro (ˉ﹃ˉ), detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(LivroModel livro) //Sobrecarga do método
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _livroRepositorio.Atualizar(livro);
                    TempData["MensagemSucesso"] = @"Livro atualizado com sucesso \(￣︶￣*\))";
                    return RedirectToAction("Index");
                }
                return View(livro);
            }
            catch (System.Exception error)
            {
                TempData["MensagemErro"] = $@"Erro ao atualuzar o livro (ˉ﹃ˉ), detalhes do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _livroRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Livro apagado com sucesso ༼ つ ◕_◕ ༽つ";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["MensagemSucesso"] = @"Não foi possivel apagar o livro ¯\(°_o)/¯";

                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $@"Não foi possivel apagar o livro ¯\(°_o)/¯ Detalhe do erro...  {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        //CSV Helper
        [HttpGet]
        public IActionResult Importar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Importar(FileModel file)
        {
            //file.FormFile.

            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.FormFile.CopyToAsync(stream);
            }
            _csvParser.ReadCSV(filePath);
            return View();
            //long size = files.Sum(f => f.Length); 
            //foreach (var formFile in file.FormFile) 
            //{ 
            //    if (formFile.Length > 0) 
            //    { 
            //        var filePath = Path.GetTempFileName(); 
                    
            //        using (var stream = System.IO.File.Create(filePath)) 
            //        { 
            //            await formFile.CopyToAsync(stream); 
            //        } 
            //    } 
            //}
            //return Ok(new { count = files.Count, size });
        }

        //public IActionResult UnicoArquivo(IFormFile file)
        //{
        //    csvParser.ReadCSV(file);
        //    return RedirectToAction("Importar");
        //}


        //Exportar
        public IActionResult Exportar()
        {
            string path = @"C:\Users\lylourenco\Downloads\livroExportado.csv";
            _csvParser.WriteCSV(path);
            return RedirectToAction("Index");
        }
    }
}
