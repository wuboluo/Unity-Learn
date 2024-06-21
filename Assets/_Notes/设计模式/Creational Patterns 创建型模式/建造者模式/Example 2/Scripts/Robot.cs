namespace Yang.DesignPattern.Builder.Example2
{
    public class Robot : IRobotPlan
    {
        private string Head { get; set; }
        private string Torso { get; set; }
        private string Arms { get; set; }
        private string Legs { get; set; }

        public void SetRobotHead(string head)
        {
            Head = head;
        }

        public void SetRobotTorso(string torso)
        {
            Torso = torso;
        }

        public void SetRobotArms(string arms)
        {
            Arms = arms;
        }

        public void SetRobotLegs(string legs)
        {
            Legs = legs;
        }

        public string ToStringEX()
        {
            return $"Head: {Head}, torso: {Torso}, Arms: {Arms}, legs: {Legs}";
        }
    }
}