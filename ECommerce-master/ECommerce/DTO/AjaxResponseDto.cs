using System;

namespace ECommerce.DTO
{
    public class AjaxResponseDto
    {
        public Guid AjaxResponseUid = Guid.NewGuid();
        public dynamic Dynamic { get; set; }

    }
}
