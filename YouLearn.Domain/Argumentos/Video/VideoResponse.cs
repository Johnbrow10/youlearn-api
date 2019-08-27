using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entidades;

namespace YouLearn.Domain.Argumentos.Video
{
    public class VideoResponse
    {
        public string NomeCanal { get; set; }
        //public Guid IdPlayList { get; set; }
        public Guid? IdPlayList { get; set; }
        public string NomePlayList { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Thumbmail { get; set; }
        public string IdVideoYoutube { get; set; }
        public int OrdemNaPlayList { get; set; }
        public string Url { get; set; }

        public static explicit operator VideoResponse(Entidades.Video entidade)
        {
            return new VideoResponse()
            {
                Descricao = entidade.Descricao,
                Url = string.Concat("https://www.youtube.com/embed/", entidade.IdVideoYoutube),
                NomeCanal = entidade.Canal.Nome,
                IdVideoYoutube = entidade.IdVideoYoutube,
                Thumbmail = string.Concat("https://img.youtube.com/vi/", entidade.IdVideoYoutube, "/mqdefault.jpg"),
                Titulo = entidade.Titulo,
                //IdPlayList = entidade.PlayList.Id,
                IdPlayList = entidade.PlayList?.Id,
                NomePlayList = entidade.PlayList?.Nome,
                OrdemNaPlayList = entidade.OrdemNaPlayList

            };
        }
    }
}
