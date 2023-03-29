using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complex = System.Numerics.Complex;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;


/// <summary>
/// Class <c>Quantum</c> acts as a library to perform quantum operations and work with states.
/// </summary>
public class Quantum
{
    
    public static readonly Matrix<Complex> PauliX = DenseMatrix.OfArray(new Complex[,] {
        {0,1},
        {1,0}});

    public static readonly Matrix<Complex> PauliY = DenseMatrix.OfArray(new Complex[,] {
        {0,new Complex(0, -1)},
        {new Complex(0,1),0}});

    public static readonly Matrix<Complex> PauliZ = DenseMatrix.OfArray(new Complex[,] {
        {1,0},
        {0,-1}});

    public static readonly Matrix<Complex> Hadamard = DenseMatrix.OfArray(new Complex[,] {
        {1/Mathf.Sqrt(2f),1/Mathf.Sqrt(2f)},
        {1/Mathf.Sqrt(2f),-1/Mathf.Sqrt(2f)}}); // Hadamard

    public static readonly Vector<Complex> State0 = DenseVector.OfArray(new Complex[] {1,0});
    public static readonly Vector<Complex> State1 = DenseVector.OfArray(new Complex[] {0,1});

    /// <summary>
    /// Method <c>MatrixMult</c> multiplies matrix A and B.
    /// </summary>
    public static Matrix<Complex> MatrixMult(Matrix<Complex> a, Matrix<Complex> b){

        return a.Multiply(b);
    }

    /// <summary>
    /// Method <c>MatrixVectorMult</c> applies matrix A to vector B.
    /// </summary>
    public static Vector<Complex> MatrixVectorMult(Matrix<Complex> a, Vector<Complex> b){
        return a.Multiply(b);
    }

    /// <summary>
    /// Method <c>QubitState</c> creates a qubit state from two input values.
    /// </summary>
    public static Vector<Complex> QubitState(Complex a, Complex b){
        Vector<Complex> State = DenseVector.OfArray(new Complex[] {a,b});
        return State;
    }
    
}
