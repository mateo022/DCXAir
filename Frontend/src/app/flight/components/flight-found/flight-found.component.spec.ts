import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightFoundComponent } from './flight-found.component';

describe('FlightFoundComponent', () => {
  let component: FlightFoundComponent;
  let fixture: ComponentFixture<FlightFoundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FlightFoundComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FlightFoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
