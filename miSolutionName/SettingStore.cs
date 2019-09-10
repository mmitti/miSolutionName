using Reactive.Bindings;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Reactive.Bindings.Extensions;
using System.Linq.Expressions;

namespace miSolutionName
{
    public class SettingStore
    {
        private UserOptions UserOption;
        public SettingStore(Options option, UserOptions userOption)
        {
            ReadOnlyReactiveProperty<Color> convert(ReactiveProperty<Color?> r, Color d)
            {
                return r.CombineLatest(UserOptionEnable, (c, e) => {
                     return (e && c.HasValue) ? c.Value : d;
                 }).ToReadOnlyReactiveProperty(d);
            }
            ReactiveProperty<Color?> to_property_as_sync(Expression<Func<UserOptions, string>> func)
            {
                return UserOption.ToReactivePropertyAsSynchronized<UserOptions, string, Color?>(
                    func,
                    convert: x => 
                        Common.TryConvertColor(x),
                    convertBack: x => 
                        x.HasValue ? Common.ConvertColor(x.Value) : ""
                );
            }
            UserOption = userOption;
            
            UserActiveBackground = to_property_as_sync(x => x.ActiveBackground);
            UserActiveForeground = to_property_as_sync(x => x.ActiveForeground);
            UserInActiveBackground = to_property_as_sync(x => x.InActiveBackground);
            UserInActiveForeground = to_property_as_sync(x => x.InActiveForeground);

            UserOptionEnable = UserOption.ToReactivePropertyAsSynchronized(x => x.IsEnabled);
            IsPenguin = option.Penguin;

            ActiveBackground = convert(UserActiveBackground, option.DefaultActiveBackgroundColor);
            ActiveForeground = convert(UserActiveForeground, option.DefaultActiveForegroundColor);
            InActiveBackground = convert(UserInActiveBackground, option.DefaultInActiveBackgroundColor);
            InActiveForeground = convert(UserInActiveForeground, option.DefaultInActiveForegroundColor);
        }

        public ReadOnlyReactiveProperty<Color> ActiveForeground { get; private set; }
        public ReadOnlyReactiveProperty<Color> ActiveBackground { get; private set; }
        public ReadOnlyReactiveProperty<Color> InActiveForeground { get; private set; }
        public ReadOnlyReactiveProperty<Color> InActiveBackground { get; private set; }
        public ReactiveProperty<Color?> UserActiveForeground { get; private set; }
        public ReactiveProperty<Color?> UserActiveBackground { get; private set; }
        public ReactiveProperty<Color?> UserInActiveForeground { get; private set; }
        public ReactiveProperty<Color?> UserInActiveBackground { get; private set; }
        public ReactiveProperty<bool> UserOptionEnable { get; private set; }
        public bool IsPenguin { get; private set; }
        

    }
}
