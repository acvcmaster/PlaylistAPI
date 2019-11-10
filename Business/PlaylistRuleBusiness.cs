using System.Linq;
using PlaylistAPI.Models;

namespace PlaylistAPI.Business
{
    public class PlaylistRuleBusiness : BaseBusiness<PlaylistRule>
    {
        public PlaylistRuleBusiness() : base(null)
        {
        }

        public IQueryable<object> GetPlaylistRules(int id)
        {
            var playlistRuleSet = Context.ArquireDbSet<PlaylistRule>();
            var ruleSet = Context.ArquireDbSet<Rule>();
            var propertySet = Context.ArquireDbSet<Property>();
            var comparatorSet = Context.ArquireDbSet<Comparator>();

            return (from rule in playlistRuleSet
                join r in ruleSet on rule.RuleId equals r.Id
                join p in propertySet on r.PropertyId equals p.Id
                join c in comparatorSet on r.ComparatorId equals c.Id
                where rule.PlaylistId == id
                select new PlaylistRuleCompleteModel() {
                    Id = rule.Id,
                    Creation = rule.Creation,
                    LastModification = rule.LastModification,
                    Active = rule.Active,
                    Property = p.Name,
                    PropertyType = p.Type,
                    Operator = c.Operator,
                    OperatorDescription = c.Description,
                    Data = rule.Data
                });
        }
    }
}