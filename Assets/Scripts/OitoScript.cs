using UnityEngine;
using System.Collections;

public class OitoScript : CarBehaviour {

	// OITO

	//limiares RODAS STRENGTH
	public float limiarMinimo = 0, limiarMaximo = 9999;

	//limites  OUTPUT
	public float limiteInferior = 0, limiteSuperior = 9999; 

	void Update()
	{
		//InvertedLinear - nome funcao
		//Gaussian - nome funcao

		//Read sensor values
		float leftSensor = LeftLD.GetLinearOutput (); // Gaussiana? 
		float rightSensor = RightLD.GetLinearOutput ();

		// strength
		if (leftSensor < limiarMinimo)
			leftSensor = limiarMinimo;
		else if (leftSensor > limiarMaximo)
			leftSensor = limiarMaximo;

		if (rightSensor < limiarMinimo)
			rightSensor = limiarMinimo;
		else if (rightSensor > limiarMaximo)
			rightSensor = limiarMaximo;


		// read sensor values - blocks
		float leftSensorBlock = LeftBD.GetLinearOutput ();
		float rightSensorBlock = RightBD.GetLinearOutput ();

		if (leftSensorBlock < limiarMinimo)
			leftSensorBlock = limiarMinimo;
		else if (leftSensorBlock > limiarMaximo)
			leftSensorBlock = limiarMaximo;

		if (rightSensorBlock < limiarMinimo)
			rightSensorBlock = limiarMinimo;
		else if (rightSensorBlock > limiarMaximo)
			rightSensorBlock = limiarMaximo;


		//Calculate target motor values
		// troca sensor de roda
		m_LeftWheelSpeed = ((rightSensor + leftSensorBlock) * MaxSpeed);
		m_RightWheelSpeed = ((leftSensor + rightSensorBlock) * MaxSpeed);

		// output
		if (m_LeftWheelSpeed < limiteInferior)
			m_LeftWheelSpeed = limiteInferior;
		else if (m_LeftWheelSpeed > limiteSuperior)
			m_LeftWheelSpeed = limiteSuperior;

		if (m_RightWheelSpeed < limiteInferior)
			m_RightWheelSpeed = limiteInferior;
		else if (m_RightWheelSpeed > limiteSuperior)
			m_RightWheelSpeed = limiteSuperior;
	}
}