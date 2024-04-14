import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchRouteComponent } from './search-route.component';

describe('SearchRouteComponent', () => {
  let component: SearchRouteComponent;
  let fixture: ComponentFixture<SearchRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SearchRouteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
