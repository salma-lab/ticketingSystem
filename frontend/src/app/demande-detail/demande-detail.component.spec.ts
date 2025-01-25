import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemandeDetailComponent } from './demande-detail.component';

describe('DemandeDetailComponent', () => {
  let component: DemandeDetailComponent;
  let fixture: ComponentFixture<DemandeDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DemandeDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DemandeDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
