namespace Advanced_Combat_Tracker
{
    using System;

    internal class ChatShareEntry
    {
        public readonly string character;
        public readonly string data;
        public readonly string type;

        public ChatShareEntry(string Character, string Type, string Data)
        {
            this.character = Character;
            this.type = Type;
            this.data = Data;
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return (this.character + " (" + this.type + ")");
        }
    }
}

