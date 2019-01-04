using UnityEngine;
using System.Collections;

public class CarBehaviour4c : CarBehaviour {

	// MEDROSO GAUSSIANA

	//limiares (threshold) RODAS STRENGTH
	public float limiarMinimo = 0, limiarMaximo = 9999;

	//limites OUTPUT
	public float limiteInferior = 0, limiteSuperior = 9999;

	void Update()
	{
		//Read sensor values
		float leftSensor = LeftLD.GetGaussianOutput ();
		float rightSensor = RightLD.GetGaussianOutput ();

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
		m_LeftWheelSpeed = ((leftSensor + leftSensorBlock) * MaxSpeed);
		m_RightWheelSpeed = ((rightSensor + rightSensorBlock) * MaxSpeed);

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
