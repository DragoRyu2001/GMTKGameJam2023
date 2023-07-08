using System.Collections.Generic;

namespace DragoRyu.DevTools.Sequence
{
    public class Sequencer
    {
        private List<ISequence> _sequence;

        public Sequencer AddSequence(ISequence sequence)
        {
            _sequence.Add(sequence);
            return this;
        }
        public void FireSequence()
        {
            foreach (var sequence in _sequence)
            {
                sequence.Trigger();
            }
        }
        public void ResetSequence()
        {
            foreach (var sequence in _sequence)
            {
                sequence.Reset();
            }
        }
    }
}