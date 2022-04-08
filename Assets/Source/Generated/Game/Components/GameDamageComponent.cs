//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DamageComponent damage { get { return (DamageComponent)GetComponent(GameComponentsLookup.Damage); } }
    public bool hasDamage { get { return HasComponent(GameComponentsLookup.Damage); } }

    public void AddDamage(int newValue) {
        var index = GameComponentsLookup.Damage;
        var component = (DamageComponent)CreateComponent(index, typeof(DamageComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDamage(int newValue) {
        var index = GameComponentsLookup.Damage;
        var component = (DamageComponent)CreateComponent(index, typeof(DamageComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDamage() {
        RemoveComponent(GameComponentsLookup.Damage);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherDamage;

    public static Entitas.IMatcher<GameEntity> Damage {
        get {
            if (_matcherDamage == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Damage);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDamage = matcher;
            }

            return _matcherDamage;
        }
    }
}
