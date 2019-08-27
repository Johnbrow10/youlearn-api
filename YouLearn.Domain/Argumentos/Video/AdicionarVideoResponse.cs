using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Domain.Argumentos.Video
{
    public class AdicionarVideoResponse
    {
        public AdicionarVideoResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
