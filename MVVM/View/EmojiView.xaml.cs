using SkyLauncherRemastered.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkyLauncherRemastered.MVVM.View
{
    public partial class EmojiView : UserControl
    {
        public static EmojiView EmojiInstance { get; private set; }

        private enum EmojiCategory
        {
            SEARCH,
            HISTORY,
            SMILEY,
            ANIMALS,
            FLOWER,
            SPORT,
            LIGHTBULB,
            HASHTAG
        }
        private EmojiCategory category = EmojiCategory.SMILEY;

        public EmojiView()
        {
            InitializeComponent();
            refreshGrid(category);
            EmojiInstance = this;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name.ToString())
            {
                case "SEARCH": category = EmojiCategory.SMILEY; break;
                case "HISTORY": category = EmojiCategory.HISTORY; break;
                case "SMILEY": category = EmojiCategory.SMILEY; break;
                case "ANIMALS": category = EmojiCategory.ANIMALS; break;
                case "FLOWER": category = EmojiCategory.FLOWER; break;
                case "SPORT": category = EmojiCategory.SPORT; break;
                case "LIGHTBULB": category = EmojiCategory.LIGHTBULB; break;
                case "HASHTAG": category = EmojiCategory.HASHTAG; break;
                default: category = EmojiCategory.SMILEY; break;
            }
            refreshGrid(category);
        }

        private void refreshGrid(EmojiCategory category)
        {
            Grid myGrid = _EmojyGrid;

            myGrid.Children.Clear();
            myGrid.ColumnDefinitions.Clear();
            myGrid.RowDefinitions.Clear();

            String[] smi = CreateEmojiList(category);
            int columnCount = 9; //Spalten
            int tempC = 0;
            int rowCount;
            int tempR = 0;

            rowCount = (smi.Length / columnCount);

            //myGrid.Height = rowCount * 75;
            myGrid.Width = 720;
            myGrid.ShowGridLines = false;
            myGrid.HorizontalAlignment = HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Top;

            for (int b = 0; b <= columnCount; b++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int b = 0; b <= rowCount; b++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < smi.Length; i++)
            {
                if (tempC > columnCount)
                {
                    tempC = 0;
                    tempR++;
                    if (tempR > rowCount)
                    {
                        return;
                    }
                }

                Emoji.Wpf.TextBlock txt1 = new Emoji.Wpf.TextBlock();
                txt1.Text = smi[i];
                txt1.FontSize = 35;
                txt1.TextAlignment = TextAlignment.Center;
                txt1.HorizontalAlignment = HorizontalAlignment.Center;
                txt1.MouseLeftButtonDown += ButDeletOnPreviewMouseDown;

                Grid.SetColumnSpan(txt1, 1);
                Grid.SetColumn(txt1, tempC);
                Grid.SetRow(txt1, tempR);

                myGrid.Children.Add(txt1);

                tempC++;
            }
        }

        private async void ButDeletOnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Emoji.Wpf.TextBlock textBlock = (Emoji.Wpf.TextBlock)sender;
            try
            {
                await copyToClipboard(textBlock.Text, true);
            } catch (System.ArgumentException ex)
            {
                return;
            }
            
        }

        private String[] CreateEmojiList(EmojiCategory category)
        {
            String SmileyList = null;
            switch (category)
            {
                case EmojiCategory.SMILEY:
                    SmileyList =   "😀,😃,😄,😁,😆,😅,🤣,😂,🙂,🙃,😉,😊,😇,🥰,😍,🤩,😘,😗,☺️,😚,😙,🥲,😋,😛,😜,🤪,😝,🤑,🤗,🤭,🤫,🤔,🤐,🤨,😐,😑,😶," +
                                "😏,😒,🙄,😬,😮,💨,🤥,😌,😔,😪,🤤,😴,😷,🤒,🤕,🤢,🤮,🤧,🥵,🥶,🥴,😵,💫,🤯,🤠,🥳,🥸,😎,🤓,🧐,😕,😟,🙁,☹️,😮,😯,😳," +
                                "🥺,😦,😧,😨,😰,😥,😢,😭,😱,😖,😣,😞,😓,😩,😫,🥱,😤,😡,😠,🤬,😈,👿,💀,☠️,💩,🤡,👹,👺,👻,👽,👾,🤖,😺,😸,😹,😻,😼,😽," +
                                "🙀,😿,😾,🙈,🙉,🙊";
                    break;
                case EmojiCategory.ANIMALS:
                    SmileyList =   "🐵,🐒,🦍,🦧,🐶,🐕,🦮,🐕,‍🦺,🐩,🐺,🦊,🦝,🐱,🐈,🦁,🐯,🐅,🐆,🐴,🐎,🦄,🦓,🦌," +
                                "🦬,🐮,🐂,🐃,🐄,🐷,🐖,🐗,🐽,🐏,🐑,🐐,🐪,🐫,🦙,🦒,🐘,🦣,🦏,🦛,🐭,🐁,🐀,🐹,🐰,🐇,🦫,🐿,🦔," +
                                "🦇,🐻,‍❄️,🐨,🐼,🦥,🦦,🦨,🦘,🦡,🐾,🦃,🐔,🐓,🐣,🐤,🐥,🐦,🐧,🕊,🦅,🦆,🦢,🦉,🦤,🪶,🦩,🦚," +
                                "🦜,🐸,🐊,🐢,🦎,🐍,🐲,🐉,🦕,🦖,🐳,🐋,🐬,🦭,🐟,🐠,🐡,🦈,🐙,🐚,🐌,🦋,🐛,🐜,🐝,🪲,🐞,🦗,🪳,🕷,🕸,🦂,🦟,🪰,🪱,🦠";
                    break;
                case EmojiCategory.FLOWER:
                    SmileyList =   "💐,🌸,💮,🏵,🌹,🥀,🌺,🌻,🌼,🌷,🌱,🪴,🌲,🌳,🌴,🌵,🌾,🌿,☘️,🍀,🍁,🍂,🍃," +
                                "🍇,🍈,🍉,🍊,🍋,🍌,🍍,🥭,🍎,🍏,🍐,🍑,🍒,🍓,🫐,🥝,🍅,🫒,🥥,🥑,🍆,🥔,🥕,🌽,🌶,🫑," +
                                "🥒,🥬,🥦,🧄,🧅,🍄,🥜,🌰,🍞,🥐,🥖,🫓,🥨,🥯,🥞,🧇,🧀,🍖,🍗,🥩,🥓,🍔,🍟,🍕,🌭," +
                                "🥪,🌮,🌯,🫔,🥙,🧆,🥚,🍳,🥘,🍲,🫕,🥣,🥗,🍿,🧈,🧂,🥫,🍱,🍘,🍙,🍚,🍛,🍜,🍝,🍠,🍢," +
                                "🍣,🍤,🍥,🥮,🍡,🥟,🥠,🥡,🦀,🦞,🦐,🦑,🦪,🍦,🍧,🍨,🍩,🍪,🎂,🍰,🧁,🥧,🍫,🍬,🍭," +
                                "🍮,🍯,🍼,🥛,☕,🫖,🍵,🍶,🍾,🍷,🍸,🍹,🍺,🍻,🥂,🥃,🥤,🧋,🧃,🧉,🧊,🥢,🍽,🍴,🥄,🔪,🏺";
                    break;
                case EmojiCategory.SPORT:
                    SmileyList =   "🎃,🎄,🎆,🎇,🧨,✨,🎈,🎉,🎊,🎋,🎍,🎎,🎏,🎐,🎑,🧧,🎀,🎁,🎗,🎟,🎫,🎖,🏆,🏅,🥇,🥈," +
                                "🥉,⚽,⚾,🥎,🏀,🏐,🏈,🏉,🎾,🥏,🎳,🏏,🏑,🏒,🥍,🏓,🏸,🥊,🥋,🥅,⛳,⛸️," +
                                "🎣,🤿,🎽,🎿,🛷,🥌,🎯,🪀,🪁,🎱,🔮,🪄,🧿,🎮,🕹,🎰,🎲,🧩,🧸,🪅,🪆,♠️,♥️,♦️,♣️,♟️,🃏,🀄,🎴,🎭,🖼,🎨,🧵,🪡,🧶,🪢," +
                                "🌍,🌎,🌏,🌐,🗺,🗾,🧭,🏔,⛰️,🌋,🗻,🏕,🏖,🏜,🏝,🏞,🏟,🏛,🏗,🧱,🪨,🪵,🛖,🏘,🏚," +
                                "🏠,🏡,🏢,🏣,🏤,🏥,🏦,🏨,🏩,🏪,🏫,🏬,🏭,🏯,🏰,💒,🗼,🗽,⛪,🕌,🛕,🕍,⛩️,🕋,⛲,⛺," +
                                "🌁,🌃,🏙,🌄,🌅,🌆,🌇,🌉,♨️,🎠,🎡,🎢,💈,🎪,🚂,🚃,🚄,🚅,🚆,🚇,🚈,🚉,🚊,🚝,🚞,🚋," +
                                "🚌,🚍,🚎,🚐,🚑,🚒,🚓,🚔,🚕,🚖,🚗,🚘,🚙,🛻,🚚,🚛,🚜,🏎,🏍,🛵,🦽,🦼,🛺,🚲,🛴," +
                                "🛹,🛼,🚏,🛣,🛤,🛢,⛽,🚨,🚥,🚦,🛑,🚧,⚓,⛵,🛶,🚤,🛳,⛴️,🛥,🚢,✈️,🛩,🛫," +
                                "🛬,🪂,💺,🚁,🚟,🚠,🚡,🛰,🚀,🛸,🛎,🧳,⌛,⏳,⌚,⏰,⏱️,⏲️,🕰,🕥,🌑,🌕,🌛,🌜," +
                                "🌚,🌙,🌡,☀️,🌝,🌞,🪐,⭐,🌟,🌠,🌌,☁️,⛅,⛈️,🌤,🌥,🌦,🌧,🌨,🌩,🌪,🌫,🌬,🌀," +
                                "🌈,🌂,☂️,☔,⛱️,⚡,❄️,☃️,⛄,☄️,🔥,💧,🌊";
                    break;
                case EmojiCategory.LIGHTBULB:
                    SmileyList =   "⚕️,🏫,⚖️,🌾,🍳,🔧,🏭,💼,🔬,💻,🎤,🎨‍,✈️,🚀,🚒,👮,‍🕵,💂," +
                                "👷,🤴,👸,👳,‍👲,🧕,🤵,‍👰,‍🤰,🤱,🍼,👼,🎅,🤶,🎄,🦸," +
                                "🦹,‍🧙,‍🧚,‍🧛,‍🧜,‍🧝,‍🧞,‍🧟,‍💆,💇,‍🚶,🧍,‍🧎,🦯,🦼,🦽," +
                                "🏃,‍💃,🕺,🕴,👯,‍🧖,🧗,🤺,🏇,⛷️,🏂,🏌,🤽,‍🤾,🤹,🧘,🛀,🛌,🤝,👭,💏,💑,👪,‍" +
                                "🗣,👤,👥,🫂,👣" +
                                "👓,🕶,🥽,🥼,🦺,👔,👕,👖,🧣,🧤,🧥,🧦,👗,👘,🥻,🩱,🩲,🩳,👙,👚,👛,👜,👝,🛍,🎒," +
                                "🩴,👞,👟,🥾,🥿,👠,👡,🩰,👢,👑,👒,🎩,🎓,🧢,🪖,⛑️,📿,💄,💍,💎,🔇,🔈,🔉,🔊,📢,📣," +
                                "📯,🔔,🔕,🎼,🎵,🎶,🎙,🎚,🎛,🎤,🎧,📻,🎷,🪗,🎸,🎹,🎺,🎻,🪕,🥁,🪘,📱,📲,☎️," +
                                "📞,📟,📠,🔋,🔌,💻,🖥,🖨,⌨️,🖱,🖲,💽,💾,💿,📀,🧮,🎥,🎞,📽,🎬,📺," +
                                "📷,📸,📹,📼,🔍,🔎,🕯,💡,🔦,🏮,🪔,📔,📕,📖,📗,📘,📙,📚,📓,📒,📃,📜,📄," +
                                "📰,🗞,📑,🔖,🏷,💰,🪙,💴,💵,💶,💷,💸,💳,🧾,💹,✉️,📧,📨,📩,📤,📥,📦,📫,📪," +
                                "📬,📭,📮,🗳,✏️,✒️,🖋,🖊,🖌,🖍,📝,💼,📁,📂,🗂,📅,📆,🗒,🗓,📇,📈," +
                                "📉,📊,📋,📌,📍,📎,🖇,📏,📐,✂️,🗃,🗄,🗑,🔒,🔓,🔏,🔐,🔑,🗝,🔨,🪓,⛏️,⚒️," +
                                "🛠,🗡,⚔️,🔫,🪃,🏹,🛡,🪚,🔧,🪛,🔩,⚙️,🗜,⚖️,🦯,🔗,⛓️,🪝,🧰,🧲,🪜,⚗️,🧪,🧫,🧬," +
                                "🔬,🔭,📡,💉,🩸,💊,🩹,🩺,🚪,🛗,🪞,🪟,🛏,🛋,🪑,🚽,🪠,🚿,🛁,🪤,🪒,🧴,🧷,🧹,🧺,🧻,🪣,🧼,🪥,🧽," +
                                "🧯,🛒,🚬,⚰️,🪦,⚱️,🗿,🪧," +
                                "🚰,⚠️,⛔,🚫,🚳,🚭,🚯,🚱,🚷,📵,🔞,☢️,☣️,🔙,🔚,🔛,🔜,🔝,🔅,🔆,✖️,➕,➖,➗,♾️,‼️,⁉️,❓,❔,❕,❗," +
                                "💲,♻️,⚜️,🔱,⭕,✅,✔️,❌,❎,〽️,#️⃣,*️⃣,0️⃣,1️⃣,2️⃣,3️⃣,4️⃣,5️⃣,6️⃣,7️⃣,8️⃣,9️⃣,🔟,🆗,🅿,🆘,🆚,🔴,🟠,🟡,🟢,🔵,🟣,🟤,⚫," +
                                "⚪,🟥,🟧,🟨,🟩,🟦,🟪,🟫,⬛,⬜";
                    break;
                case EmojiCategory.HASHTAG:
                    SmileyList =   "💋,💌,💘,💝,💖,💗,💓,💞,💕,💟,❣️,💔,❤️‍,🔥,❤️‍,🩹,❤️,🧡,💛,💚,💙,💜,🤎,🖤,🤍,💯,💢,💥,💫,💦,💨,🕳,💣," +
                                "💬,👁,🗨,🗯,💭,💤,🧠,🫀,🫁,🦷,🦴,👀,👁,👅,👄,👶,🧒,👦,👧,👱,👨,🧔,👩,👱,‍🧓,👴,👵,🙍,🙎,‍🙅,‍🙆,‍💁,‍🙋,‍🧏,‍🙇,‍🤦," +
                                "🤷,👋,🤚,🖐,✋,🖖,👌,🤌,🤏,✌️,🤞,🤟,🤘,🤙,👈,👉,👆,🖕,👇,☝️,👍," +
                                "👎,✊,👊,🤛,🤜,👏,🙌,👐,🤲,🤝,🙏,✍️,💅,🤳,💪,🦾,🦿,🦵,🦶,👂,🦻,👃," +
                                "🏁,🚩,🎌,🏴,🏳,🏳,🌈,🏳,⚧️,🏴,☠️,🇦🇨,🇦🇩,🇦🇪,🇦🇫,🇦🇬,🇦🇮,🇦🇱,🇦🇲,🇦🇴,🇦🇶,🇦🇷,🇦🇸,🇦🇹,🇦🇺,🇦🇼,🇦🇽,🇦🇿,🇧🇦,🇧🇧,🇧🇩,🇧🇪,🇧🇫,🇧🇬,🇧🇭,🇧🇮,🇧🇯,🇧🇱,🇧🇲," +
                                "🇧🇳,🇧🇴,🇧🇶,🇧🇷,🇧🇸,🇧🇹,🇧🇻,🇧🇼,🇧🇾,🇧🇿,🇨🇦,🇨🇨,🇨🇩,🇨🇫,🇨🇬,🇨🇭,🇨🇮,🇨🇰,🇨🇱,🇨🇲,🇨🇳,🇨🇴,🇨🇵,🇨🇷,🇨🇺,🇨🇻,🇨🇼,🇨🇽,🇨🇾,🇨🇿,🇩🇪,🇩🇬,🇩🇯,🇩🇰,🇩🇲,🇩🇴,🇩🇿,🇪🇦,🇪🇨,🇪🇪,🇪🇬,🇪🇭,🇪🇷,🇪🇸," +
                                "🇪🇹,🇪🇺,🇫🇮,🇫🇯,🇫🇰,🇫🇲,🇫🇴,🇫🇷,🇬🇦,🇬🇧,🇬🇩,🇬🇪,🇬🇫,🇬🇬,🇬🇭,🇬🇮,🇬🇱,🇬🇲,🇬🇳,🇬🇵,🇬🇶,🇬🇷,🇬🇸,🇬🇹,🇬🇺,🇬🇼,🇬🇾,🇭🇰,🇭🇲,🇭🇳,🇭🇷,🇭🇹,🇭🇺,🇮🇨,🇮🇩,🇮🇪,🇮🇱,🇮🇲,🇮🇳,🇮🇴,🇮🇶,🇮🇷,🇮🇸,🇮🇹,🇯🇪," +
                                "🇯🇲,🇯🇴,🇯🇵,🇰🇪,🇰🇬,🇰🇭,🇰🇮,🇰🇲,🇰🇳,🇰🇵,🇰🇷,🇰🇼,🇰🇾,🇰🇿,🇱🇦,🇱🇧,🇱🇨,🇱🇮,🇱🇰,🇱🇷,🇱🇸,🇱🇹,🇱🇺,🇱🇻,🇱🇾,🇲🇦,🇲🇨,🇲🇩,🇲🇪,🇲🇫,🇲🇬,🇲🇭,🇲🇰,🇲🇱,🇲🇲,🇲🇳,🇲🇴,🇲🇵,🇲🇶,🇲🇷,🇲🇸,🇲🇹,🇲🇺," +
                                "🇲🇻,🇲🇼,🇲🇽,🇲🇾,🇲🇿,🇳🇦,🇳🇨,🇳🇪,🇳🇫,🇳🇬,🇳🇮,🇳🇱,🇳🇴,🇳🇵,🇳🇷,🇳🇺,🇳🇿,🇴🇲,🇵🇦,🇵🇪,🇵🇫,🇵🇬,🇵🇭,🇵🇰,🇵🇱,🇵🇲,🇵🇳,🇵🇷,🇵🇸,🇵🇹,🇵🇼,🇵🇾,🇶🇦,🇷🇪,🇷🇴,🇷🇸,🇷🇺,🇷🇼,🇸🇦,🇸🇧,🇸🇨,🇸🇩,🇸🇪,🇸🇬," +
                                "🇸🇭,🇸🇮,🇸🇯,🇸🇰,🇸🇱,🇸🇲,🇸🇳,🇸🇴,🇸🇷,🇸🇸,🇸🇹,🇸🇻,🇸🇽,🇸🇾,🇸🇿,🇹🇦,🇹🇨,🇹🇩,🇹🇫,🇹🇬,🇹🇭,🇹🇯,🇹🇰,🇹🇱,🇹🇲,🇹🇳,🇹🇴,🇹🇷,🇹🇹,🇹🇻,🇹🇼,🇹🇿,🇺🇦,🇺🇬,🇺🇲,🇺🇳,🇺🇸,🇺🇾,🇺🇿,🇻🇦,🇻🇨,🇻🇪,🇻🇬,🇻🇮,🇻🇳,🇻🇺,🇼🇫,🇼🇸," +
                                "🇽🇰,🇾🇪,🇾🇹,🇿🇦,🇿🇲,🇿🇼,🏴󠁧󠁢󠁥󠁮󠁧󠁿,🏴󠁧󠁢󠁳󠁣󠁴󠁿,🏴󠁧󠁢󠁷󠁬󠁳󠁿";
                    break;
                case EmojiCategory.HISTORY:
                    SmileyList = Settings.Default.History.Substring(0,Settings.Default.History.Length-1);
                    break;
            }
            return SmileyList.Split(',');
        }

        public static async Task copyToClipboard(String emoji, bool addToHistory)
        {
            System.Windows.Clipboard.SetText(emoji);

            EmojiView emojiView = EmojiInstance;
            Button copyButton = emojiView._CopyButton;
            copyButton.Content = "📋 Copied to Clipboard";
            copyButton.Visibility = Visibility.Visible;
            if (addToHistory) AddToHistory(emoji);
            await Task.Delay(1500);
            copyButton.Visibility = Visibility.Hidden;
        }

        private static void AddToHistory(string emoji)
        {
            string history = Settings.Default.History;

            if (history.IndexOf(emoji) != -1) return;

            history = emoji + "," + history;
            if (history.Length > 30*9)
            {
                Settings.Default.History = history.Substring(0, 30*9);
            } else {
                Settings.Default.History = history;
            }
            Settings.Default.Save();
        }
    }
}
