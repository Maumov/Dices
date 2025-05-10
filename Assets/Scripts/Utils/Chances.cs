using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Chances : MonoBehaviour
{
    public int permutationsWith2Dices;
    public int permutationsWith3Dices;
    public int permutationsWith4Dices;
    public int permutationsWith5Dices;

    [Header( "Pairs" )]
    public int amountOfPairsWith2Dices;
    public int amountOfPairsWith3Dices;
    public int amountOfPairsWith4Dices;
    public int amountOfPairsWith5Dices;

    public float chanceForPairsWith2Dices;
    public float chanceForPairsWith3Dices;
    public float chanceForPairsWith4Dices;
    public float chanceForPairsWith5Dices;

    [Header( "Trio" )]
    public int amountOfTrioWith3Dices;
    public int amountOfTrioWith4Dices;
    public int amountOfTrioWith5Dices;

    public float chanceForTrioWithThreeDices;
    public float chanceForTrioWithFourDices;
    public float chanceForTrioWithFiveDices;

    [Header( "Quad" )]
    public int amountOfQuadWith4Dices;
    public int amountOfQuadWith5Dices;

    public float chanceForQuadWithFourDices;
    public float chanceForQuadWithFiveDices;

    [Header( "Quintuple" )]
    public int amountOfQuntupleWith5Dices;

    public float chanceForQuintupleWithFiveDices;

    [Header( "2 Pairs" )]
    public int amountOf2PairsWith4Dices;
    public int amountOf2PairsWith5Dices;

    public float chanceFor2PairsWithFourDices;
    public float chanceFor2PairsWithFiveDices;

    [Header( "Full House" )]
    public int amountOfFullHousesWith5Dices;

    public float chanceForFullHouseWithFiveDices;

    [Header( "Max Denominator" )]
    public int numerator;
    public int denominator;
    public int maxDenominator;
    public int newNumerator, newDenominator;

    [ContextMenu( "Calculate Denominator" )]
    void FindMaxDenominator()
    {
        for ( int i = 1 ; i <= numerator ; i++ )
        {
            if ( ( numerator % i == 0 ) && ( denominator % i == 0 ) )
            {
                maxDenominator = i;
            }

        }
        newNumerator = numerator / maxDenominator;
        newDenominator = denominator / maxDenominator;
    }

    IEnumerator FindChances()
    {
        yield return StartCoroutine( FindChancesForPairs() );
        yield return StartCoroutine( FindChancesForThrice() );
        yield return StartCoroutine( FindChancesForQuad() );
        yield return StartCoroutine( FindChancesForQuintuple() );
        yield return StartCoroutine( FindChancesDoublePairs() );
        yield return StartCoroutine( FindChancesWithFullHouse() );
    }

    IEnumerator FindChancesForPairs()
    {
        float posibleOutcomes = 0f;
        float pairsFound = 0f;
        //2 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int j = 1 ; j <= 6 ; j++ )
            {
                posibleOutcomes++;
                if ( IsPair( i, j ) )
                {
                    pairsFound++;
                }
            }
        }
        permutationsWith2Dices = ( int ) posibleOutcomes;
        amountOfPairsWith2Dices = ( int ) pairsFound;
        chanceForPairsWith2Dices = pairsFound / posibleOutcomes;
        yield return null;

        posibleOutcomes = 0f;
        pairsFound = 0f;
        //3 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    posibleOutcomes++;
                    if ( IsPair( i, j, k ) )
                    {
                        pairsFound++;
                    }
                }

            }
        }
        permutationsWith3Dices = ( int ) posibleOutcomes;
        amountOfPairsWith3Dices = ( int ) pairsFound;
        chanceForPairsWith3Dices = pairsFound / posibleOutcomes;
        yield return null;
        posibleOutcomes = 0f;
        pairsFound = 0f;
        //4 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int l = 1 ; l <= 6 ; l++ )
                    {
                        posibleOutcomes++;
                        if ( IsPair( i, j, k, l ) )
                        {
                            pairsFound++;
                        }
                    }
                }

            }
        }
        permutationsWith4Dices = ( int ) posibleOutcomes;
        amountOfPairsWith4Dices = ( int ) pairsFound;
        chanceForPairsWith4Dices = pairsFound / posibleOutcomes;
        yield return null;

        posibleOutcomes = 0f;
        pairsFound = 0f;
        //5 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int m = 1 ; m <= 6 ; m++ )
                    {
                        for ( int l = 1 ; l <= 6 ; l++ )
                        {
                            posibleOutcomes++;
                            if ( IsPair( i, j, k, l, m ) )
                            {
                                pairsFound++;
                            }
                        }
                    }
                }

            }
        }
        permutationsWith5Dices = ( int ) posibleOutcomes;
        amountOfPairsWith5Dices = ( int ) pairsFound;
        chanceForPairsWith5Dices = pairsFound / posibleOutcomes;
        yield return null;
    }
    IEnumerator FindChancesForThrice()
    {

        float posibleOutcomes = 0f;
        float thriceFound = 0f;
        //3 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    posibleOutcomes++;
                    if ( IsThrice( i, j, k ) )
                    {
                        thriceFound++;
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith3Dices )
        {
            Debug.Log( "Posible outcomes with 3 dices is different" );
        }

        amountOfTrioWith3Dices = ( int ) thriceFound;
        chanceForTrioWithThreeDices = thriceFound / posibleOutcomes;
        yield return null;
        posibleOutcomes = 0f;
        thriceFound = 0f;
        //4 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int l = 1 ; l <= 6 ; l++ )
                    {
                        posibleOutcomes++;
                        if ( IsThrice( i, j, k, l ) )
                        {
                            thriceFound++;
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith4Dices )
        {
            Debug.Log( "Posible outcomes with 4 dices is different" );
        }
        amountOfTrioWith4Dices = ( int ) thriceFound;
        chanceForTrioWithFourDices = thriceFound / posibleOutcomes;
        yield return null;

        posibleOutcomes = 0f;
        thriceFound = 0f;
        //5 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int m = 1 ; m <= 6 ; m++ )
                    {
                        for ( int l = 1 ; l <= 6 ; l++ )
                        {
                            posibleOutcomes++;
                            if ( IsThrice( i, j, k, l, m ) )
                            {
                                thriceFound++;
                            }
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith5Dices )
        {
            Debug.Log( "Posible outcomes with 5 dices is different" );
        }
        amountOfTrioWith5Dices = ( int ) thriceFound;
        chanceForTrioWithFiveDices = thriceFound / posibleOutcomes;
        yield return null;
    }
    IEnumerator FindChancesForQuad()
    {
        float posibleOutcomes = 0f;
        float quadFound = 0f;
        //4 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int l = 1 ; l <= 6 ; l++ )
                    {
                        posibleOutcomes++;
                        if ( IsQuad( i, j, k, l ) )
                        {
                            quadFound++;
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith4Dices )
        {
            Debug.Log( "Posible outcomes with 5 dices is different" );
        }
        amountOfQuadWith4Dices = ( int ) quadFound;
        chanceForQuadWithFourDices = quadFound / posibleOutcomes;
        yield return null;

        posibleOutcomes = 0f;
        quadFound = 0f;
        //5 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int m = 1 ; m <= 6 ; m++ )
                    {
                        for ( int l = 1 ; l <= 6 ; l++ )
                        {
                            posibleOutcomes++;
                            if ( IsQuad( i, j, k, l, m ) )
                            {
                                quadFound++;
                            }
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith5Dices )
        {
            Debug.Log( "Posible outcomes with 5 dices is different" );
        }
        amountOfQuadWith5Dices = ( int ) quadFound;
        chanceForQuadWithFiveDices = quadFound / posibleOutcomes;
        yield return null;
    }
    IEnumerator FindChancesForQuintuple()
    {
        float posibleOutcomes = 0f;
        float quintupleFound = 0f;
        //4 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int m = 1 ; m <= 6 ; m++ )
                    {
                        for ( int l = 1 ; l <= 6 ; l++ )
                        {
                            posibleOutcomes++;
                            if ( IsQuintuple( i, j, k, l, m ) )
                            {
                                quintupleFound++;
                            }
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith5Dices )
        {
            Debug.Log( "Posible outcomes with 5 dices is different" );
        }
        amountOfQuntupleWith5Dices = ( int ) quintupleFound;
        chanceForQuintupleWithFiveDices = quintupleFound / posibleOutcomes;
        yield return null;
    }
    IEnumerator FindChancesDoublePairs()
    {
        float posibleOutcomes = 0f;
        float doublePairsFound = 0f;
        //4 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int l = 1 ; l <= 6 ; l++ )
                    {
                        posibleOutcomes++;
                        if ( DoublePairs( i, j, k, l ) )
                        {
                            doublePairsFound++;
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith4Dices )
        {
            Debug.Log( "Posible outcomes with 5 dices is different" );
        }
        amountOf2PairsWith4Dices = ( int ) doublePairsFound;
        chanceFor2PairsWithFourDices = doublePairsFound / posibleOutcomes;
        yield return null;

        posibleOutcomes = 0f;
        doublePairsFound = 0f;
        //5 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int m = 1 ; m <= 6 ; m++ )
                    {
                        for ( int l = 1 ; l <= 6 ; l++ )
                        {
                            posibleOutcomes++;
                            if ( DoublePairs( i, j, k, l, m ) )
                            {
                                doublePairsFound++;
                            }
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith5Dices )
        {
            Debug.Log( "Posible outcomes with 5 dices is different" );
        }
        amountOf2PairsWith5Dices = ( int ) doublePairsFound;
        chanceFor2PairsWithFiveDices = doublePairsFound / posibleOutcomes;
        yield return null;
    }
    IEnumerator FindChancesWithFullHouse()
    {
        float posibleOutcomes = 0f;
        float fullHouseFound = 0f;
        //4 Dices
        for ( int i = 1 ; i <= 6 ; i++ )
        {
            for ( int k = 1 ; k <= 6 ; k++ )
            {
                for ( int j = 1 ; j <= 6 ; j++ )
                {
                    for ( int m = 1 ; m <= 6 ; m++ )
                    {
                        for ( int l = 1 ; l <= 6 ; l++ )
                        {
                            posibleOutcomes++;
                            if ( FullHouse( i, j, k, l, m ) )
                            {
                                fullHouseFound++;
                            }
                        }
                    }
                }

            }
        }
        if ( posibleOutcomes != permutationsWith5Dices )
        {
            Debug.Log( "Posible outcomes with 5 dices is different" );
        }
        amountOfFullHousesWith5Dices = ( int ) fullHouseFound;
        chanceForQuintupleWithFiveDices = fullHouseFound / posibleOutcomes;
        yield return null;
    }
    public bool FullHouse( int i, int j, int k, int l, int m )
    {
        return !IsQuintuple( i, j, k, l, m ) && (
               ( IsThrice( i, j, k ) && IsPair( l, m ) ) ||
               ( IsThrice( m, i, j ) && IsPair( k, l ) ) ||
               ( IsThrice( l, m, i ) && IsPair( j, k ) ) ||
               ( IsThrice( k, l, m ) && IsPair( i, j ) ) ||
               ( IsThrice( j, k, l ) && IsPair( m, i ) ) ||
               ( IsThrice( j, k, m ) && IsPair( i, l ) ) ||
               ( IsThrice( j, l, m ) && IsPair( i, k ) ) ||
               ( IsThrice( i, k, m ) && IsPair( j, l ) ) ||
               ( IsThrice( i, k, l ) && IsPair( j, m ) ) ||
               ( IsThrice( i, j, l ) && IsPair( k, m ) )
               );
    }
    public bool DoublePairs( int i, int j, int k, int l, int m )
    {
        return !FullHouse( i, j, k, l, m ) && !IsThrice( i, j, k, l, m ) &&
              ( DoublePairs( i, j, k, l ) ||
                DoublePairs( m, j, k, l ) ||
                DoublePairs( i, m, k, l ) ||
                DoublePairs( i, j, m, l ) ||
                DoublePairs( i, j, k, m ) );
    }
    public bool DoublePairs( int i, int k, int l, int m )
    {
        return ( ( i == k ) && ( l == m ) && ( i != l ) ) ||
               ( ( i == l ) && ( k == m ) && ( i != k ) ) ||
               ( ( i == m ) && ( k == l ) && ( i != k ) );
    }
    public bool IsQuintuple( int i, int j, int k, int l, int m )
    {
        return ( i == j && i == k && i == l && i == m );
    }
    public bool IsQuad( int i, int j, int k, int l, int m )
    {
        return !IsQuintuple( i, j, k, l, m ) && ( IsQuad( i, j, k, l ) || IsQuad( j, k, l, m ) || IsQuad( i, j, l, m ) || IsQuad( i, j, k, m ) || IsQuad( i, k, l, m ) );
    }
    public bool IsThrice( int i, int j, int k, int l, int m )
    {
        return !FullHouse( i, j, k, l, m ) && !IsQuintuple( i, j, k, l, m ) && !IsQuad( i, j, k, l, m ) && ( IsThrice( i, j, k, l ) || IsThrice( j, k, l, m ) || IsThrice( i, k, l, m ) || IsThrice( i, j, l, m ) || IsThrice( i, j, k, m ) );
    }
    public bool IsPair( int i, int j, int k, int l, int m )
    {
        return !IsQuintuple( i, j, k, l, m ) && !DoublePairs( i, j, k, l, m ) && !IsQuad( i, j, k, l, m ) && !IsThrice( i, j, k, l, m ) && ( IsPair( i, j, k, l ) || IsPair( m, j, k, l ) || IsPair( i, m, k, l ) || IsPair( i, j, m, l ) || IsPair( i, j, k, m ) );
    }
    public bool IsQuad( int i, int j, int k, int l )
    {
        return ( i == j && i == k && i == l );
    }
    public bool IsThrice( int i, int j, int k, int l )
    {
        return !IsQuad( i, j, k, l ) && ( IsThrice( i, j, k ) || IsThrice( i, j, l ) || IsThrice( i, k, l ) || IsThrice( j, k, l ) );
    }
    public bool IsPair( int i, int j, int k, int l )
    {
        return !IsQuad( i, j, k, l ) && !DoublePairs( i, j, k, l ) && !IsThrice( i, j, k, l ) && ( IsPair( i, j, k ) || IsPair( i, k, l ) || IsPair( j, k, l ) || IsPair( i, j, l ) );
    }
    public bool IsThrice( int i, int j, int k )
    {
        return ( i == j && i == k );
    }
    public bool IsPair( int i, int j, int k )
    {
        return !IsThrice( i, j, k ) && ( IsPair( i, j ) || IsPair( i, k ) || IsPair( j, k ) );
    }
    public bool IsPair( int i, int j )
    {
        return i == j;
    }

    public bool ContainsPair( int[] dices )
    {
        if ( dices.Length < 2 )
        {
            return false;
        }
        for ( int i = 0 ; i < dices.Length - 1 ; i++ )
        {
            for ( int j = i + 1 ; j < dices.Length ; j++ )
            {
                if ( dices[ i ] == dices[ j ] )
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool ContainsThrice( int[] dices )
    {
        if ( dices.Length < 3 )
        {
            return false;
        }
        for ( int i = 0 ; i < dices.Length - 1 ; i++ )
        {
            int count = 1;
            for ( int j = i + 1 ; j < dices.Length ; j++ )
            {
                if ( dices[ i ] == dices[ j ] )
                {
                    count++;
                    if ( count == 3 )
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool ContainsQuad( int[] dices )
    {
        if ( dices.Length < 4 )
        {
            return false;
        }
        for ( int i = 0 ; i < dices.Length - 1 ; i++ )
        {
            int count = 1;
            for ( int j = i + 1 ; j < dices.Length ; j++ )
            {
                if ( dices[ i ] == dices[ j ] )
                {
                    count++;
                    if ( count == 4 )
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool ContainsPenta( int[] dices )
    {
        if ( dices.Length < 5 )
        {
            return false;
        }
        for ( int i = 0 ; i < dices.Length - 1 ; i++ )
        {
            int count = 1;
            for ( int j = i + 1 ; j < dices.Length ; j++ )
            {
                if ( dices[ i ] == dices[ j ] )
                {
                    count++;
                    if ( count == 5 )
                    {
                        return true;
                    }
                }

            }
        }
        return false;
    }

    public int UniqueValues( int[] dices )
    {
        return dices.Distinct().Count();
    }

    bool ContainsNumber( int[] dices, int number)
    {
        return dices.Contains( number );
    }

    bool ContainsNumberAmountOfTimes( int[] dices, int number, int times )
    {
        int count = dices.Count( n => n == number );
        return count == times;
    }
    bool ContainsNumberAtLeastAmountOfTimes( int[] dices, int number, int times )
    {
        int count = dices.Count( n => n == number );
        return count >= times;
    }


    public Dictionary<BETS, bool> GetHand( int[] dices)
    {
        bool containsOne = ContainsNumber( dices, 1 );
        bool containsTwo = ContainsNumber( dices, 2 );
        bool containsThree = ContainsNumber( dices, 3 );
        bool containsFour = ContainsNumber( dices, 4 );
        bool containsFive = ContainsNumber( dices, 5 );
        bool containsSix = ContainsNumber( dices, 6 );

        bool IsPairOf1 = ContainsNumberAmountOfTimes( dices, 1 , 2 );
        bool IsPairOf2 = ContainsNumberAmountOfTimes( dices, 2 , 2 );
        bool IsPairOf3 = ContainsNumberAmountOfTimes( dices, 3 , 2 );
        bool IsPairOf4 = ContainsNumberAmountOfTimes( dices, 4 , 2 );
        bool IsPairOf5 = ContainsNumberAmountOfTimes( dices, 5 , 2 );
        bool IsPairOf6 = ContainsNumberAmountOfTimes( dices, 6 , 2 );

        bool IsThriceOf1 = ContainsNumberAmountOfTimes( dices, 1 , 3 );
        bool IsThriceOf2 = ContainsNumberAmountOfTimes( dices, 2 , 3 );
        bool IsThriceOf3 = ContainsNumberAmountOfTimes( dices, 3 , 3 );
        bool IsThriceOf4 = ContainsNumberAmountOfTimes( dices, 4 , 3 );
        bool IsThriceOf5 = ContainsNumberAmountOfTimes( dices, 5 , 3 );
        bool IsThriceOf6 = ContainsNumberAmountOfTimes( dices, 6 , 3 );

        bool IsQuadOf1 = ContainsNumberAmountOfTimes( dices, 1, 4 );
        bool IsQuadOf2 = ContainsNumberAmountOfTimes( dices, 2, 4 );
        bool IsQuadOf3 = ContainsNumberAmountOfTimes( dices, 3, 4 );
        bool IsQuadOf4 = ContainsNumberAmountOfTimes( dices, 4, 4 );
        bool IsQuadOf5 = ContainsNumberAmountOfTimes( dices, 5, 4 );
        bool IsQuadOf6 = ContainsNumberAmountOfTimes( dices, 6, 4 );

        bool IsPentaOf1 = ContainsNumberAmountOfTimes( dices, 1, 5 );
        bool IsPentaOf2 = ContainsNumberAmountOfTimes( dices, 2, 5 );
        bool IsPentaOf3 = ContainsNumberAmountOfTimes( dices, 3, 5 );
        bool IsPentaOf4 = ContainsNumberAmountOfTimes( dices, 4, 5 );
        bool IsPentaOf5 = ContainsNumberAmountOfTimes( dices, 5, 5 );
        bool IsPentaOf6 = ContainsNumberAmountOfTimes( dices, 6, 5 );

        bool containsPenta = IsPentaOf1 || IsPentaOf2 || IsPentaOf3 || IsPentaOf4 || IsPentaOf5 || IsPentaOf6;
        bool containsQuad = IsQuadOf1 || IsQuadOf2 || IsQuadOf3 || IsQuadOf4 || IsQuadOf5 || IsQuadOf6 || containsPenta;
        bool containsThrice = IsThriceOf1 || IsThriceOf2 || IsThriceOf3 || IsThriceOf4 || IsThriceOf5 || IsThriceOf6 || containsQuad;
        bool containsPair = IsPairOf1 || IsPairOf2 || IsPairOf3 || IsPairOf4 || IsPairOf5 || IsPairOf6 || containsThrice; 
        
        int  uniques = UniqueValues( dices );
        bool containsDoublePair = false;
        containsDoublePair |= ( dices.Length >= 4 && uniques == 2 );
        containsDoublePair |= ( uniques == 3 && !containsThrice && dices.Length == 5 );

        bool isFullHouse = ( uniques == 2 && dices.Length == 5);
        bool isDoublePair = containsDoublePair && !isFullHouse;
        bool isPenta = containsPenta;
        bool isQuad = containsQuad && !containsPenta;
        bool isThrice = containsThrice && !containsQuad&& !isFullHouse;
        bool isPair = containsPair && !containsDoublePair && !containsThrice;

        Dictionary<BETS, bool> keyValuePairs = new Dictionary<BETS, bool>();
        keyValuePairs.Add( BETS.Contains1, containsOne );
        keyValuePairs.Add( BETS.Contains2, containsTwo );
        keyValuePairs.Add( BETS.Contains3, containsThree );
        keyValuePairs.Add( BETS.Contains4, containsFour );
        keyValuePairs.Add( BETS.Contains5, containsFive );
        keyValuePairs.Add( BETS.Contains6, containsSix );

        keyValuePairs.Add( BETS.Containspair1, IsPairOf1 || IsThriceOf1 || IsQuadOf1 || IsPentaOf1);
        keyValuePairs.Add( BETS.Containspair2, IsPairOf2 || IsThriceOf2 || IsQuadOf2 || IsPentaOf2 );
        keyValuePairs.Add( BETS.Containspair3, IsPairOf3 || IsThriceOf3 || IsQuadOf3 || IsPentaOf3 );
        keyValuePairs.Add( BETS.Containspair4, IsPairOf4 || IsThriceOf4 || IsQuadOf4 || IsPentaOf4 );
        keyValuePairs.Add( BETS.Containspair5, IsPairOf5 || IsThriceOf5 || IsQuadOf5 || IsPentaOf5 );
        keyValuePairs.Add( BETS.Containspair6, IsPairOf6 || IsThriceOf6 || IsQuadOf6 || IsPentaOf6 );

        keyValuePairs.Add( BETS.Containsthrice1, IsThriceOf1 || IsQuadOf1 || IsPentaOf1 );
        keyValuePairs.Add( BETS.Containsthrice2, IsThriceOf2 || IsQuadOf2 || IsPentaOf2 );
        keyValuePairs.Add( BETS.Containsthrice3, IsThriceOf3 || IsQuadOf3 || IsPentaOf3 );
        keyValuePairs.Add( BETS.Containsthrice4, IsThriceOf4 || IsQuadOf4 || IsPentaOf4 );
        keyValuePairs.Add( BETS.Containsthrice5, IsThriceOf5 || IsQuadOf5 || IsPentaOf5 );
        keyValuePairs.Add( BETS.Containsthrice6, IsThriceOf6 || IsQuadOf6 || IsPentaOf6 );

        keyValuePairs.Add( BETS.ContainsQuad1, IsQuadOf1 || IsPentaOf1 );
        keyValuePairs.Add( BETS.ContainsQuad2, IsQuadOf2 || IsPentaOf2 );
        keyValuePairs.Add( BETS.ContainsQuad3, IsQuadOf3 || IsPentaOf3 );
        keyValuePairs.Add( BETS.ContainsQuad4, IsQuadOf4 || IsPentaOf4 );
        keyValuePairs.Add( BETS.ContainsQuad5, IsQuadOf5 || IsPentaOf5 );
        keyValuePairs.Add( BETS.ContainsQuad6, IsQuadOf6 || IsPentaOf6 );

        keyValuePairs.Add( BETS.ContainsPenta1, IsPentaOf1 );
        keyValuePairs.Add( BETS.ContainsPenta2, IsPentaOf2 );
        keyValuePairs.Add( BETS.ContainsPenta3, IsPentaOf3 );
        keyValuePairs.Add( BETS.ContainsPenta4, IsPentaOf4 );
        keyValuePairs.Add( BETS.ContainsPenta5, IsPentaOf5 );
        keyValuePairs.Add( BETS.ContainsPenta6, IsPentaOf6 );

        keyValuePairs.Add( BETS.ContainsPair, containsPair);
        keyValuePairs.Add( BETS.ContainsThrice, containsThrice );
        keyValuePairs.Add( BETS.ContainsQuad, containsQuad);
        keyValuePairs.Add( BETS.ContainsPenta, containsPenta );
        keyValuePairs.Add( BETS.ContainsDoublePair, containsDoublePair );

        keyValuePairs.Add( BETS.IsFullHouse, isFullHouse );
        keyValuePairs.Add( BETS.IsPair, isPair );
        keyValuePairs.Add( BETS.IsThrice, isThrice );
        keyValuePairs.Add( BETS.IsQuad, isQuad);
        keyValuePairs.Add( BETS.IsPenta, isPenta );
        keyValuePairs.Add( BETS.IsDoublePair, isDoublePair );

        return keyValuePairs;
    }
}
