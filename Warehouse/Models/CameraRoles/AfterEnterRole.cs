﻿using NLog;
using Warehouse.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles
{
    public class AfterEnterRole : CameraRoleBase
    {
        private readonly WaitingListsService _waitingListsService;

        public AfterEnterRole(ILogger logger, WaitingListsService waitingListsService) : base(logger)
        {
            Name = "После въезда";
            Description = "Подтверждение въезда машины на территорию";
            _waitingListsService = waitingListsService;
        }

        protected override void OnExecute(Camera camera)
        {
            // TODO: Проверить номер по спискам (_waitingListsService)
            // TODO: Сменить статус - "Ожидает первое взвешивание"
            // TODO: Отсыпать в лог, если статус машины не "На въезде"
            // (Не корректный номер машины или она не попала на первую камеру
            // и охранник пустил машину не через программу)
        }
    }
}
