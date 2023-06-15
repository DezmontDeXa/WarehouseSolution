using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.CameraRoles;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.ExprectedStates
{
    public static class ExpectedStatesConfig
    {
        public static IReadOnlyDictionary<ICameraRoleBase, IReadOnlyList<ICarStateBase>> Expectes { get; }

        static ExpectedStatesConfig()
        {
            7   Выезд(Герцена)
1   Пропуск Б.Хмельницкого
2   После въезда(Б.Хмельницкого)
3   На весовой(Б.Хмельницкого)
5   На весовой 2(Б.Хмельницкого)
4   Выезд(Б.Хмельницкого)
6   Въезд герцена


            var expectes = new Dictionary<ICameraRoleBase, IReadOnlyList<ICarStateBase>>();
            expectes.Add(new BeforeEnterRole(), new List<ICarStateBase>()
            {
                new AwaitingState(),
                new ChangingAreaState()
            });
            expectes.Add(2, new List<ICarStateBase>()
            {
                new OnEnterState()
            });
            expectes.Add(3, new List<ICarStateBase>()
            {
                new AwaitingWeighingState()
            });
            expectes.Add(5, new List<ICarStateBase>()
            {
                new AwaitingWeighingState()
            });
            expectes.Add(5, new List<ICarStateBase>()
            {
                new AwaitingState(),
                new ChangingAreaState()
            });
            expectes.Add(6, new List<ICarStateBase>()
            {
                new AwaitingState(),
                new ChangingAreaState()
            });
            expectes.Add(7, new List<ICarStateBase>()
            {
                new AwaitingState(),
                new ChangingAreaState()
            });

            Expectes = expectes;
        }
    }
}