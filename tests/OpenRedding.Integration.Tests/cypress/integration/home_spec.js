describe('Home page', () => {
    beforeEach(() => cy.visit('/'));
    
    it('should validate all search boxes and buttons are loaded on initial page navigation', () => {
        // Act/Assert
        cy.contains('An open data portal for Redding, for the people, by the people.');
        cy.get('#salary-search-name');
        cy.get('#salary-search-job-title');
        cy.contains('All Agencies');
        cy.contains('All Statuses');
        cy.get('#salary-search-button');
        cy.get('.bmc-button')
    });

    it('should display the \'Coming soon...\' modal for budget panel when clicked', () => {
        // Arrange
        const budgetInputButton = cy.get('#search-budget-button-option');

        // Act
        budgetInputButton.click({force: true});

        // Assert
        cy.contains('Coming soon...');
        cy.contains('This feature is currently under development. Think you can help? We\'re always looking for collaborators!');
    });

    it('should display the \'Coming soon...\' modal for the zoning panel when clicked', () => {
        // Arrange
        const zoningInputButton = cy.get('#search-zoning-button-option');

        // Act
        zoningInputButton.click({force: true});

        // Assert
        cy.contains('Coming soon...');
        cy.contains('This feature is currently under development. Think you can help? We\'re always looking for collaborators!');
    });
    
    it('should enable the \'Search\' button for salaries when an valid value is entered for \'Name\'', () => {
        // Arrange, assert button is enabled
        const validName = 'Valid Name';
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
        
        // Act
        cy.get('#salary-search-name').type(validName);
        
        // Assert
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
    });

    it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Name\'', () => {
        // Arrange, assert button is enabled
        const invalidName = 'Inv@al#d N@m$';
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
        
        // Act
        cy.get('#salary-search-name').type(invalidName);
        
        // Assert
        cy.get('#salary-search-button').should('be.disabled');
        cy.get('#salary-search-name').should('have.class', 'is-invalid');
    });
    
    it('should enable the \'Search\' button for salaries when an valid value is entered for \'Job Title\'', () => {
        // Arrange, assert button is enabled
        const validJobTitle = 'Valid Job Title';
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
        
        // Act
        cy.get('#salary-search-job-title').type(validJobTitle);
        
        // Assert
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
    });

    it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Job Title\'', () => {
        // Arrange, assert button is enabled
        const invalidJobTitle = 'Inv@al#d J0b t*tl3';
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
        
        // Act
        cy.get('#salary-search-job-title').type(invalidJobTitle);
        
        // Assert
        cy.get('#salary-search-button').should('be.disabled');
        cy.get('#salary-search-job-title').should('have.class', 'is-invalid');
    });

    it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Job Title\' with a valid value for \'Name\'', () => {
        // Arrange, assert button is enabled
        const invalidJobTitle = 'Inv@al#d J0b t*tl3';
        const validName = 'Valid Name';
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
        cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
        
        // Act
        cy.get('#salary-search-job-title').type(invalidJobTitle);
        cy.get('#salary-search-name').type(validName);
        
        // Assert
        cy.get('#salary-search-button').should('be.disabled');
        cy.get('#salary-search-job-title').should('have.class', 'is-invalid');
        cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
    });

    it('should disable the \'Search\' button for salaries when an invalid value is entered for \'Name\' with a valid value for \'Job Title\'', () => {
        // Arrange, assert button is enabled
        const validJobTitle = 'Valid Job Title';
        const invalidName = '!@#123abc';
        cy.get('#salary-search-button').should('be.enabled');
        cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
        cy.get('#salary-search-name').should('not.have.class', 'is-invalid');
        
        // Act
        cy.get('#salary-search-job-title').type(validJobTitle);
        cy.get('#salary-search-name').type(invalidName);
        
        // Assert
        cy.get('#salary-search-button').should('be.disabled');
        cy.get('#salary-search-job-title').should('not.have.class', 'is-invalid');
        cy.get('#salary-search-name').should('have.class', 'is-invalid');
    });
});