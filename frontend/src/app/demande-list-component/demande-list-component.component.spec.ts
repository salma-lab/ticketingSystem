import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemandeListComponentComponent } from './demande-list-component.component';

describe('DemandeListComponentComponent', () => {
  let component: DemandeListComponentComponent;
  let fixture: ComponentFixture<DemandeListComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DemandeListComponentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DemandeListComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
