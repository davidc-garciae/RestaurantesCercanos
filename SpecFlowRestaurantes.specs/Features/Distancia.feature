Feature: Distancia
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SpecFlowRestaurantes.specs/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Get the closest restaurant
	Given the latitude is 6.0
	And the longitude is -75.0
	When the distances are calculated
	And the distances are sorted
	Then the closest one should be 32971.24113472873

Scenario: Get the closest restaurant city
	Given the latitude is 10.0
	And the longitude is -69.0
	When the distances are calculated
	And the distances are sorted
	Then the closest city should be Cocorote

Scenario: Get the closest restaurant country
	Given the latitude is 55.0
	And the longitude is -29.0
	When the distances are calculated
	And the distances are sorted
	Then the closest country should be Iceland

Scenario: Get the 5th closest restaurant
	Given the latitude is 4.7
	And the longitude is -67.8
	When the distances are calculated
	And the distances are sorted
	Then the fifth closest should be 477063.98226423736
