using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.ExprectedStates
{
    public static class ExpectedStatesConfig
    {
        public static Expect[] Expectes { get; }

        static ExpectedStatesConfig()
        {
            Expectes = new Expect[]
            {
                new Expect(
                    new BeforeEnterRole(),
                    new ICarStateBase[]
                    {
                        new AwaitingState(),
                        new ChangingAreaState()
                    }),
                new Expect(
                    new AfterEnterRole(),
                    new ICarStateBase[]
                    {
                        new OnEnterState()
                    }),
                new Expect(
                    new OnWeightingRole(),
                    new ICarStateBase[]
                    {
                        new AwaitingFirstWeighingState(),
                        new AwaitingSecondWeighingState(),
                        new WeighingState(),
                        new OnEnterState(),
                        new ExitPassGrantedState(),
                    }),
                new Expect(
                    new ExitRole(),
                    new ICarStateBase[]
                    {
                        new ExitPassGrantedState(),
                        new ExitingForChangeAreaState(),
                        new ExitingState()
                    }),
                new Expect(
                    new EnterRole(),
                    new ICarStateBase[]
                    {
                        new ChangingAreaState(),
                    }),
                new Expect(
                    new ExitFromAnotherAreaRole(),
                    new ICarStateBase[]
                    {
                        new LoadingState(),
                        new UnloadingState(),
                        new SamplingState(),
                    }),
            };
        }

        public static bool IsExpectedState(int roleId, int stateId)
        {
            return Expectes.First(x=>x.Role.Id == roleId).ExpectedStates.Any(x=>x.Id == stateId);
        }
    }
}