using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lecture_CSharpStudy
{
    // LINQ
    // 참고링크.
    // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-5.0

    public enum ClassType
    {
        Knight
        , Archer
        , Mage
    }

    public class Player
    {
        public ClassType Type { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public List<int> Items { get; set; } = new List<int>();
    }
    class Linq
    {
        List<Player> _players = new List<Player>();

        public void Execute()
        {
            Random random = new Random();
            for (int i = 0; i < 100; ++i)
            {
                ClassType type = ClassType.Knight;
                switch (random.Next(0, 3))
                {
                    case 0:
                        type = ClassType.Knight;
                        break;

                    case 1:
                        type = ClassType.Archer;
                        break;

                    case 2:
                        type = ClassType.Mage;
                        break;
                }

                Player player = new Player()
                {
                    Type = type
                    , Level = random.Next(1, 10)
                    , Hp = random.Next(100, 1000)
                    , Attack = random.Next(5, 50)
                };

                for (int j = 0; j < 5; ++j)
                {
                    player.Items.Add(random.Next(1, 101));
                }

                _players.Add(player);
            }

            // Q) Level 50 이상인 Knight만 추려내서, LEVEL을 오름차순으로 정렬.

            // NORMAL VERSION
            {
                List<Player> players = GetHighLevelKnight();
                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.Level} {player.Hp}");
                }
            }

            Console.WriteLine("-------------------------------------");
            // LINQ VERSION
            {
                // from     : foreach
                // where    : filtering. ( 조건에 부합하는 DATA만 추출 )
                // orderby  : 정렬 수행. ( DEFAULT: 오름차순   ascending / descending )
                // select   : 최종 결과 추출. ( 가공 추출 가능 )
                var players =
                    from p in _players
                    where p.Type == ClassType.Knight && p.Level >= 50
                    orderby p.Level ascending
                    select p; // select p.Hp;, select new { Hp = p.Hp, Level = p.Level * 2 }; 등

                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.Level} {player.Hp}");
                }
            }

            // 중첩 from
            // Q) 모든 아이템 목록을 추출 ( Item ID < 30 )
            {
                var playerItems =
                    from p in _players
                    from i in p.Items
                    where i < 30
                    select new { p, i };

                var li = playerItems.ToList();
            }

            // Grouping
            {
                var playersByLevel =
                    from p in _players
                    group p by p.Level into grouped
                    orderby grouped.Key
                    select new { g.Key, Players = grouped };
            }

            // Join
            // Outer Join
            {
                List<int> levels = new List<int>() { 1, 5, 10 };

                var playerLevels =
                    from p in _players
                    join l in levels
                    on p.Level equals l
                    select p;
            }

            // LINQ 표준 연산자
            {
                var players =
                    from p in _players
                    where p.Type == ClassType.Knight && p.Level >= 50
                    orderby p.Level ascending
                    select p;

                // LINQ를 함수 형식으로 바꾼 것.
                var pls = _players
                    .Where(p => p.Type == ClassType.Knight && p.Level >= 50)
                    .OrderBy(p => p.Level)
                    .Select(p => p);
            }
        }

        List<Player> GetHighLevelKnight()
        {
            List<Player> players = new List<Player>();

            foreach (Player player in _players)
            {
                if (player.Type != ClassType.Knight)
                {
                    continue;
                }

                if (player.Level < 50)
                {
                    continue;
                }

                players.Add(player);
            }

            players.Sort((lhs, rhs) => { return lhs.Level - rhs.Level; });
            return players;

        }
    }
}
